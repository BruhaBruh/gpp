using GPPlanetGQL.Exceptions;
using GPPlanetGQL.Models;
using GPPlanetGQL.Variables;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using System.Security.Claims;
using static GPPlanetGQL.GraphQL.Unions;

namespace GPPlanetGQL.GraphQL
{
    public class Subscription
    {
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<Reportmessage>> NewReportMessage(
                int id,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
                [Service] ITopicEventReceiver receiver
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).Select(u => new { UserId = u.UserId, Permissions = u.Permissions }).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            if ((user.Permissions & (Permissions.ShowReports + Permissions.All)) == 0)
            {
                var topic = $"{id}_{user.UserId}_NewReportMessage";
                return receiver.SubscribeAsync<string, Reportmessage>(topic);
            }
            else
            {
                var topic = $"{id}_A_NewReportMessage";
                return receiver.SubscribeAsync<string, Reportmessage>(topic);
            }
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<INotification>> NewNotification(
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal,
                [Service] ITopicEventReceiver receiver
            )
        {

            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (discordId == null) throw new IternalException("Ошибка авторизации");
            var user = ctx.Users.Where(u => u.DiscordId == long.Parse(discordId)).Select(u => new { UserId = u.UserId }).FirstOrDefault();
            if (user == null) throw new DoesNotExistsException($"Пользоватиля с DiscordID {discordId} не существует");
            var topic = $"{user.UserId}_NewNotification";
            return receiver.SubscribeAsync<string, INotification>(topic);
        }
    }
}
