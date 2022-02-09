
using GPPlanetGQL.Discord;
using GPPlanetGQL.Exceptions;
using GPPlanetGQL.Models;
using GPPlanetGQL.Services;
using GPPlanetGQL.Variables;

using HotChocolate.AspNetCore.Authorization;

using System.Security.Claims;

using static GPPlanetGQL.GraphQL.Types;
using static GPPlanetGQL.GraphQL.Unions;
using static GPPlanetGQL.Models.User;

namespace GPPlanetGQL.GraphQL
{
    public class Query
    {
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public async Task<IQueryable<User>> GetUsers(string? search, [Service] gpplanetContext ctx, [Service] DiscordBot bot)
        {
            var discordIds = await bot.SearchUsersInGuild(search ?? "");
            return ctx.Users.Where(u => discordIds.Contains((ulong)u.DiscordId));
        }

        [Authorize]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public IQueryable<Donateitem> GetDonateItems([Service] gpplanetContext ctx)
        {
            return ctx.Donateitems.Where(d => d.IsShow);
        }

        [UseProjection]
        public Online[] GetSiteOnlineLogs(OnlineTypes type, [Service] gpplanetContext ctx)
        {
            switch (type)
            {
                case OnlineTypes.Hour:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, nowDateTime.Hour, nowDateTime.Minute, 0, 0);
                        var online = new Online[60];
                        for (int i = -60; i < 0; i++)
                        {
                            var minTime = currentTime.AddMinutes(i);
                            var maxTime = currentTime.AddMinutes(i + 1);
                            var max = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[60 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Day:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, nowDateTime.Hour, 0, 0, 0);
                        var online = new Online[24];
                        for (int i = -24; i < 0; i++)
                        {
                            var minTime = currentTime.AddHours(i);
                            var maxTime = currentTime.AddHours(i + 1);
                            var max = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[24 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Week:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, 0, 0, 0, 0);
                        var online = new Online[7];
                        for (int i = -7; i < 0; i++)
                        {
                            var minTime = currentTime.AddDays(i);
                            var maxTime = currentTime.AddDays(i + 1);
                            var max = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[7 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Month:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, 0, 0, 0, 0);
                        var online = new Online[30];
                        for (int i = -30; i < 0; i++)
                        {
                            var minTime = currentTime.AddDays(i);
                            var maxTime = currentTime.AddDays(i + 1);
                            var max = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Siteonlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[30 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
            }
            return Array.Empty<Online>();
        }

        [UseProjection]
        public Online[] GetServerOnlineLogs(OnlineTypes type, [Service] gpplanetContext ctx)
        {
            switch (type)
            {
                case OnlineTypes.Hour:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, nowDateTime.Hour, nowDateTime.Minute, 0, 0);
                        var online = new Online[60];
                        for (int i = -60; i < 0; i++)
                        {
                            var minTime = currentTime.AddMinutes(i);
                            var maxTime = currentTime.AddMinutes(i + 1);
                            var max = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[60 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Day:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, nowDateTime.Hour, 0, 0, 0);
                        var online = new Online[24];
                        for (int i = -24; i < 0; i++)
                        {
                            var minTime = currentTime.AddHours(i);
                            var maxTime = currentTime.AddHours(i + 1);
                            var max = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[24 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Week:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, 0, 0, 0, 0);
                        var online = new Online[7];
                        for (int i = -7; i < 0; i++)
                        {
                            var minTime = currentTime.AddDays(i);
                            var maxTime = currentTime.AddDays(i + 1);
                            var max = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[7 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Month:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, 0, 0, 0, 0);
                        var online = new Online[30];
                        for (int i = -30; i < 0; i++)
                        {
                            var minTime = currentTime.AddDays(i);
                            var maxTime = currentTime.AddDays(i + 1);
                            var max = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Max();
                            var min = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Min();
                            var avg = ctx.Serveronlinelogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Online).DefaultIfEmpty().Average();
                            online[30 + i] = new Online { Min = min, Max = max, Avg = (int)Math.Floor(avg), Time = minTime };
                        }
                        return online;
                    }
            }
            return Array.Empty<Online>();
        }

        [UseProjection]
        public NewPlayer[] GetNewPlayerLogs(OnlineTypes type, [Service] gpplanetContext ctx)
        {
            switch (type)
            {
                case OnlineTypes.Hour:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, nowDateTime.Hour, nowDateTime.Minute, 0, 0);
                        var online = new NewPlayer[60];
                        for (int i = -60; i < 0; i++)
                        {
                            var minTime = currentTime.AddMinutes(i);
                            var maxTime = currentTime.AddMinutes(i + 1);
                            var max = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Max();
                            var min = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Min();
                            online[60 + i] = new NewPlayer { Inc = max-min, Total = max, Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Day:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, nowDateTime.Hour, 0, 0, 0);
                        var online = new NewPlayer[24];
                        for (int i = -24; i < 0; i++)
                        {
                            var minTime = currentTime.AddHours(i);
                            var maxTime = currentTime.AddHours(i + 1);
                            var max = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Max();
                            var min = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Min();
                            online[24 + i] = new NewPlayer { Inc = max - min, Total = max, Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Week:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, 0, 0, 0, 0);
                        var online = new NewPlayer[7];
                        for (int i = -7; i < 0; i++)
                        {
                            var minTime = currentTime.AddDays(i);
                            var maxTime = currentTime.AddDays(i + 1);
                            var max = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Max();
                            var min = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Min();
                            online[7 + i] = new NewPlayer { Inc = max - min, Total = max, Time = minTime };
                        }
                        return online;
                    }
                case OnlineTypes.Month:
                    {
                        var nowDateTime = DateTime.UtcNow;
                        var currentTime = Types.ChangeTime(DateTime.UtcNow, 0, 0, 0, 0);
                        var online = new NewPlayer[30];
                        for (int i = -30; i < 0; i++)
                        {
                            var minTime = currentTime.AddDays(i);
                            var maxTime = currentTime.AddDays(i + 1);
                            var max = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Max();
                            var min = ctx.Newplayerslogs.Where(l => l.CreatedAt > minTime && l.CreatedAt < maxTime).Select(l => l.Total).DefaultIfEmpty().Min();
                            online[30 + i] = new NewPlayer { Inc = max - min, Total = max, Time = minTime };
                        }
                        return online;
                    }
            }
            return Array.Empty<NewPlayer>();
        }

        [UseProjection]
        [UseSorting()]
        public async Task<IQueryable<User>> GetTop(
            UserTopEnum type,
            [Service] gpplanetContext ctx,
            [Service] DiscordBot bot,
            [Service] LimeService lime
        )
        {
            switch (type)
            {
                case UserTopEnum.Views:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users.Where(u => gusers.Contains((ulong)u.DiscordId)).OrderByDescending(u => u.Views).Take(50);
                    }
                case UserTopEnum.Friends:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users
                            .Where(u => gusers.Contains((ulong)u.DiscordId))
                            .OrderByDescending(u => u.FriendUsers.Count())
                            .Take(50);
                    }
                case UserTopEnum.Subscribers:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users
                            .Where(u => gusers.Contains((ulong)u.DiscordId))
                            .OrderByDescending(u => u.SubscriberUsers.Count())
                            .Take(50);
                    }
                case UserTopEnum.Years:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        var users = (await lime.GetUserDatas())
                            .Where(u => gusers.Contains(ulong.Parse(u.Key)))
                            .OrderByDescending(u => u.Value.level)
                            .Select(u => long.Parse(u.Key))
                            .Take(50)
                            .ToArray();

                        // Из-за запроса из бд нужно будет сортировать
                        return ctx.Users.Where(u => users.Contains(u.DiscordId)).Take(50);
                    }
                case UserTopEnum.Rating:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users
                            .Where(u => gusers.Contains((ulong)u.DiscordId))
                            .OrderByDescending(u => u.RatingTos.Where(r => r.Positive).Count() - u.RatingTos.Where(r => !r.Positive).Count())
                            .Take(50);
                    }
                case UserTopEnum.RatingN:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users
                            .Where(u => gusers.Contains((ulong)u.DiscordId))
                            .OrderBy(u => u.RatingTos.Where(r => r.Positive).Count() - u.RatingTos.Where(r => !r.Positive).Count())
                            .Take(50);
                    }
                case UserTopEnum.SocialPoints:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users
                            .Where(u => gusers.Contains((ulong)u.DiscordId))
                            .OrderByDescending(u => u.SocialPoints)
                            .Take(50);
                    }
                case UserTopEnum.SocialPointsN:
                    {
                        var gusers = await bot.GetUsersInGuild();
                        return ctx.Users
                            .Where(u => gusers.Contains((ulong)u.DiscordId))
                            .OrderBy(u => u.SocialPoints)
                            .Take(50);
                    }
            }

            return ctx.Users;
        }

        [Authorize]
        public UserDiscordRole[] GetDiscordRoles([Service] DiscordBot bot) => bot.GetDiscordRoles();

        [Authorize]
        public UserStatus GetStatus(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.UserId == id).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с ID {id} не существует");
            if (user.DiscordId == long.Parse(discordId)) throw new UserInputException("Вы не можете получить статус самого себя");
            var senderUser = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (senderUser == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            UserStatus status = GetUserStatus(senderUser.UserId, user.UserId, ctx);
            if (status == null) throw new IternalException("Проблема статуса");
            return status;
        }

        [Authorize]
        public User GetMe(
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
                [Service] LimeService lime,
                [Service] DiscordBot bot
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException("Пользователя не существует");
            user.LastOnline = DateTime.Now.ToUniversalTime();
            if (user.SubscriptionEndAt != null && user.SubscriptionEndAt.Value.ToUniversalTime() < DateTime.Now.ToUniversalTime())
            {
                bot.ClearRoles(user.DiscordId).GetAwaiter().GetResult();
                user.SubscriptionEndAt = null;
                if ((user.Permissions & Permissions.Lite) != 0) user.Permissions -= Permissions.Lite;
                if ((user.Permissions & Permissions.Premium) != 0) user.Permissions -= Permissions.Premium;
                if (user.Avatar.EndsWith(".gif")) user.Avatar = "";
                user.Banner = "";
            }
            ctx.SaveChanges();
            user.Trefs = lime.GetUserTref(user.DiscordId).Result;

            return user;
        }

        [Authorize]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public IQueryable<Report> GetReports([Service] gpplanetContext ctx, [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal)
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException("Пользователя не существует");
            if (((Permissions.ShowReports + Permissions.All) & user.Permissions) > 0)
            {
                return ctx.Reports;
            }
            return ctx.Reports.Where(r => r.OwnerId == user.UserId || r.ToId == user.UserId);
        }

        [Authorize]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public IQueryable<Reportmessage> GetReportMessages(
                int reportId,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.All + Permissions.ShowReports)) == 0)
            {
                var report = ctx.Reports.Where(r => r.ReportId == reportId).FirstOrDefault();
                if (report == null) throw new UserInputException($"Репорта с ID {reportId} не существует");
                if (user.UserId != report.ToId && user.UserId != report.OwnerId)
                    throw new ForbiddenException("Вы не состоите в данном репорте");
            }
            return ctx.Reportmessages.Where(r => r.ReportId == reportId);
        }

        [Authorize]
        public INotification[] GetNotifications(
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            DateTime dateTime = DateTime.Now.AddDays(-7).ToUniversalTime();
            INotification[] systemnotifications = ctx.Systemnotifications.Where(n => n.ToId == user.UserId && n.CreatedAt > dateTime).ToArray();
            INotification[] subscribernotifications = ctx.Subscribernotifications.Where(n => n.ToId == user.UserId && n.CreatedAt > dateTime).ToArray();
            INotification[] friendnotifications = ctx.Friendnotifications.Where(n => n.ToId == user.UserId && n.CreatedAt > dateTime).ToArray();
            INotification[] billnotifications = ctx.Billnotifications.Where(n => n.ToId == user.UserId && n.CreatedAt > dateTime).ToArray();
            INotification[] result = systemnotifications
                .Concat(subscribernotifications)
                .Concat(friendnotifications)
                .Concat(billnotifications).OrderByDescending(n => n.CreatedAt).ToArray();
            return result;
        }

        [Authorize]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public IQueryable<Forum> GetForums([Service] gpplanetContext ctx)
        {
            return ctx.Forums;
        }

        [Authorize]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public IQueryable<Models.Thread> GetThreads([Service] gpplanetContext ctx)
        {
            return ctx.Threads;
        }

        [Authorize]
        [UsePaging(IncludeTotalCount = true)]
        [UseProjection]
        [UseFiltering()]
        [UseSorting()]
        public IQueryable<Post> GetPosts([Service] gpplanetContext ctx)
        {
            return ctx.Posts;
        }

        // Resolvers
    }
}
