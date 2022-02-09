using GPPlanetGQL.GraphQL;
using Microsoft.EntityFrameworkCore;
using GraphQL.Server.Ui.Voyager;
using GPPlanetGQL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using GPPlanetGQL.Discord;
using GPPlanetGQL.Services;
using static GPPlanetGQL.GraphQL.Scalars;
using Microsoft.AspNetCore.CookiePolicy;
using Quartz;
using GPPlanetGQL.Jobs;

namespace GPPlanetGQL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .SetMinimumLevel(LogLevel.Warning)
                    .AddSimpleConsole()
                    .AddDebug()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);
            });

            services.AddDbContext<gpplanetContext>(opt =>
            
                opt.UseNpgsql(Environment.GetEnvironmentVariable("PGConStr") ?? "invalid"));

            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/login";
                    options.LogoutPath = "/api/auth/logout";
                    options.Events.OnSigningIn = (ctx) =>
                    {
                        ctx.CookieOptions.Expires = DateTimeOffset.UtcNow.AddDays(30);
                        return Task.CompletedTask;
                    };
                })
                .AddDiscord(options =>
                {
                    options.ClientId = Environment.GetEnvironmentVariable("DiscordClientId") ?? "client";
                    options.ClientSecret = Environment.GetEnvironmentVariable("DiscordClientSecret") ?? "secret";
                    options.CallbackPath = new PathString("/api/auth/callback");
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                });
            services.AddAuthorization();

            services.AddControllers();

            services.AddSingleton<DiscordBot>();
            services.AddSingleton<LimeService>();

            services.Configure<QuartzOptions>(Configuration.GetSection("Quartz"));

            services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true; // default: false
                options.Scheduling.OverWriteExistingData = true; // default: true
            });

            services.AddQuartz(q =>
            {
                // base quartz scheduler, job and trigger configuration
                q.SchedulerId = "Scheduler-Core";
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 5; // default 10
                });

                q.ScheduleJob<DiscordOnlineJob>(trigger => trigger
                    .WithIdentity("DiscordOnlineJob")
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(1, IntervalUnit.Minute))
                    .StartNow()
                );

                q.ScheduleJob<UserInitJob>(trigger => trigger
                    .WithIdentity("UserInitJob")
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(30, IntervalUnit.Minute))
                    .StartNow()
                );
            });

            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });


            services.AddInMemorySubscriptions();

            services
                .AddGraphQLServer()
                .AddAuthorization()
                .AddType<MediaLinkType>()
                .AddQueryType<Query>()
                .AddDefaultTransactionScopeHandler()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddProjections()
                .AddFiltering()
                .AddSorting()
                .AddErrorFilter<ErrorFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.None,
            };

            app.UseCookiePolicy(cookiePolicyOptions);

            /*app.UseCors(options =>
            {
                options.AllowAnyOrigin();
            });*/

            app.UseRouting();

            app.UseWebSockets();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", ctx =>
                {
                    ctx.Response.Redirect("graphql");
                    return Task.CompletedTask;
                });
                endpoints.MapControllers();
                endpoints.MapGraphQL("/graphql");
            });


            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
            }, "/graphql-voyager");

            _ = app.ApplicationServices.GetService<DiscordBot>().GetUsersInGuild();
        }
    }
}
