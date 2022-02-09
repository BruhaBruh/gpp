
using Discord;
using Discord.WebSocket;

using GPPlanetGQL.Exceptions;
using GPPlanetGQL.Models;
using GPPlanetGQL.Services;

using static GPPlanetGQL.Models.User;

namespace GPPlanetGQL.Discord
{
    public class DiscordBot
    {
        private readonly ILogger<DiscordBot> _logger;
        private readonly DiscordSocketClient _client;
        private readonly ulong _guildid;
        private readonly ulong _confirmedRole;
        private readonly string _token;
        private readonly long[] _blacklistedRoles;
        private readonly ulong _notifyUserId;
        private readonly ulong _premiumRole;
        private readonly ulong _liteRole;
        private readonly gpplanetContext _ctx;
        private readonly LimeService _lime;

        public DiscordBot(IConfiguration config, ILogger<DiscordBot> logger, [Service] gpplanetContext ctx, [Service] LimeService lime)
        {
            _guildid = 870190631824289863;
            _confirmedRole = 870190631824289866;
            _notifyUserId = 319124448068370432;
            _premiumRole = 900055679803473953;
            _liteRole = 900055759071637564;
            _blacklistedRoles = new long[] {
                870190631824289866,
                870190631824289863,
                900058581045489724,
                895996950921502720,
                894253135973978135,
                900058081390657537,
                881947891369930784,
                870190631824289867,
                908352094036168700
            };
            _ctx = ctx;
            _lime = lime;
            _logger = logger;
            _token = Environment.GetEnvironmentVariable("DiscordBotToken") ?? "bot token";
            var discordConfig = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMembers
            };
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.LoginAsync(TokenType.Bot, _token).GetAwaiter().GetResult();
            _client.StartAsync().GetAwaiter().GetResult();

            /*
            var total = _ctx.Users.Count();
            var online = _ctx.Users.Where(u => u.LastOnline.ToUniversalTime() > DateTime.Now.AddMinutes(-5).ToUniversalTime()).Count();
            _client.SetGameAsync($"Онлайн на сайте: {online} Всего: {total}", null, ActivityType.Playing);
            */
        }

        private Task Log(LogMessage msg)
        {
            _logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task UpdateStatus()
        {
            var discordIds = await SearchUsersInGuild("");
            var total = _ctx.Users.Count();
            var withDiscord = _ctx.Users.Where(u => discordIds.Contains((ulong)u.DiscordId)).Count();
            var online = _ctx.Users.Where(u => u.LastOnline > DateTime.Now.AddMinutes(-5).ToUniversalTime()).Count();
            _ctx.Siteonlinelogs.Add(new Siteonlinelog { Online = online });
            var limeOnline = await _lime.GetOnline();
            _ctx.Serveronlinelogs.Add(new Serveronlinelog { Online = limeOnline });
            _ctx.Newplayerslogs.Add(new Newplayerslog { Total = total });
            _ctx.SaveChanges();
            await _client.SetGameAsync($"{limeOnline}/{online}/{withDiscord}/{total} L/O/D/T", null, ActivityType.Playing);
        }

        private SocketGuild GetGuild()
        {
            var g = _client.GetGuild(_guildid);
            if (g != null) return g;
            throw new DoesNotExistsException("Сервер Discord не был найден");
        }

        private SocketGuildUser? GetGuildMember(ulong discordId)
        {
            var g = GetGuild();
            var u = g.Users.Where(u => !u.IsBot && u.Id == discordId).FirstOrDefault();
            return u;
        }

        public async Task<bool> ClearRoles(long discordId)
        {
            var u = GetGuildMember((ulong)discordId);
            if (u == null) return false;
            await u.RemoveRoleAsync(_liteRole);
            await u.RemoveRoleAsync(_premiumRole);
            return true;
        }

        public async Task<bool> SetRole(long discordId, bool isPremium)
        {
            var u = GetGuildMember((ulong)discordId);
            if (u == null) return false;
            await u.RemoveRoleAsync(_liteRole);
            await u.RemoveRoleAsync(_premiumRole);
            if (isPremium)
            {
                await u.AddRoleAsync(_premiumRole);
            }
            else
            {
                await u.AddRoleAsync(_liteRole);
            }
            return true;
        }

        public void Notify(string message)
        {
            var u = _client.GetUser(_notifyUserId);
            if (u == null) return;
            u.SendMessageAsync(message);
        }

        public EmbedBuilder getDefaultNotifyBuilder()
        {
            return new EmbedBuilder().WithFooter(new EmbedFooterBuilder().WithText("Отключить уведомления вы можете в настройках, на сайте"));
        }

        public void NotifyNewReport(Report report, long fromDiscordId, long toDiscordId)
        {
            var u = _client.GetUser((ulong)toDiscordId);
            if (u == null) return;
            var eb = getDefaultNotifyBuilder()
                .WithTitle("Отправил на вас жалобу")
                .WithAuthor(
                    new EmbedAuthorBuilder()
                        .WithIconUrl(GetAvatarByDiscordId((ulong)fromDiscordId))
                        .WithName(GetNicknameByDiscordId((ulong)fromDiscordId))
                )
                .WithDescription("Ознакомьтесь с ней")
                .WithColor(Color.Red)

                .WithUrl($"http://gpplanet.ru/r/{report.ReportId}");
            u.SendMessageAsync("", false, eb.Build());
        }

        public void NotifyNewReportMessage(Reportmessage message, long toDiscordId)
        {
            var u = _client.GetUser((ulong)toDiscordId);
            if (u == null) return;
            var eb = getDefaultNotifyBuilder()
                .WithTitle("Новое сообщение в репорте")
                .AddField("Сообщение", message.Message)
                .WithColor(Color.Teal)
                .WithUrl($"http://gpplanet.ru/r/{message.ReportId}");
            u.SendMessageAsync("", false, eb.Build());
        }

        public void NotifyNewSubscriber(User subscriber, long toDiscordId)
        {
            var u = _client.GetUser((ulong)toDiscordId);
            if (u == null) return;
            var eb = getDefaultNotifyBuilder()
                .WithTitle("Подписался на вас")
                .WithAuthor(
                    new EmbedAuthorBuilder()
                        .WithIconUrl(GetAvatarByDiscordId((ulong)subscriber.DiscordId))
                        .WithName(GetNicknameByDiscordId((ulong)subscriber.DiscordId))
                        .WithUrl($"http://gpplanet.ru/u/{subscriber.UserId}")
                )
                .WithColor(Color.Blue);
            u.SendMessageAsync("", false, eb.Build());
        }

        public void NotifyNewFriend(User subscriber, long toDiscordId)
        {
            var u = _client.GetUser((ulong)toDiscordId);
            if (u == null) return;
            var eb = getDefaultNotifyBuilder()
                .WithTitle("Принял ваш запрос в друзья")
                .WithAuthor(
                    new EmbedAuthorBuilder()
                        .WithIconUrl(GetAvatarByDiscordId((ulong)subscriber.DiscordId))
                        .WithName(GetNicknameByDiscordId((ulong)subscriber.DiscordId))
                        .WithUrl($"http://gpplanet.ru/u/{subscriber.UserId}")
                )
                .WithColor(Color.Blue);
            u.SendMessageAsync("", false, eb.Build());
        }

        public async Task<ulong[]> GetUsersInGuild()
        {
            var g = GetGuild();
            var users = g.Users;
            if (users == null) return Array.Empty<ulong>();
            var userIds = users
                .Where(m =>
                !m.IsBot &&
                m.Roles.Select(r => r.Id).ToArray().Contains(_confirmedRole))
                .Select(m => m.Id)
                .ToArray();
            if (userIds == null) return Array.Empty<ulong>();
            return userIds;
        }

        public async Task<ulong[]> SearchUsersInGuild(string search)
        {
            if (search == "") return await GetUsersInGuild();
            var g = GetGuild();
            var users = await g.SearchUsersAsync(search);
            var userIds = users
                .Where(m =>
                !m.IsBot &&
                m.RoleIds.Contains(_confirmedRole))
                .Select(m => m.Id)
                .ToArray();
            if (userIds == null) return Array.Empty<ulong>();
            return userIds;
        }

        public UserDiscordRole[] GetDiscordRoles()
        {
            var g = GetGuild();
            return g.Roles
                .Select(r => new UserDiscordRole
                {
                    Id = (long)r.Id,
                    Name = r.Name,
                    Position = r.Position,
                    Hoist = r.IsHoisted,
                    Color = r.Color.ToString()
                })
                .Where(r => !_blacklistedRoles.Contains(r.Id)).ToArray();
        }

        public string GetNicknameByDiscordId(ulong discordId)
        {
            var u = GetGuildMember(discordId);
            if (u != null) return u.Nickname ?? u.Username;
            var du = _client.GetUser(discordId);
            if (du == null) return "Пользователь не найден";
            return du.Username;
        }

        public UserDiscordRole[] GetDiscordRolesByDiscordId(ulong discordId)
        {
            var u = GetGuildMember(discordId);
            if (u == null) return Array.Empty<UserDiscordRole>();
            return u.Roles
                .Select(r => new UserDiscordRole
                {
                    Id = (long)r.Id,
                    Name = r.Name,
                    Position = r.Position,
                    Hoist = r.IsHoisted,
                    Color = r.Color.ToString()
                })
                .Where(r => !_blacklistedRoles.Contains(r.Id)).ToArray();
        }

        public string GetTagByDiscordId(ulong discordId)
        {
            var u = GetGuildMember(discordId);
            if (u != null) return $"{u.Username}#{u.Discriminator}";
            var du = _client.GetUser(discordId);
            if (du == null) return "Пользователь не найден";
            return $"{du.Username}#{du.Discriminator}";
        }

        public string GetAvatarByDiscordId(ulong discordId)
        {
            var u = GetGuildMember(discordId);
            if (u != null) return u.GetAvatarUrl(ImageFormat.Auto, 256) ?? "";
            var du = _client.GetUser(discordId);
            if (du == null) return "";
            return du.GetAvatarUrl(ImageFormat.Auto, 256) ?? "";
        }
    }
}
