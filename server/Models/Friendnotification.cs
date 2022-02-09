using System;
using System.Collections.Generic;
using static GPPlanetGQL.GraphQL.Unions;

namespace GPPlanetGQL.Models
{
    public partial class Friendnotification : INotification
    {
        public int FriendnotificationId { get; set; }
        public int ToId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int FriendRsId { get; set; }

        public virtual Friend FriendRs { get; set; } = null!;
        public virtual User To { get; set; } = null!;
        public Friend GetFriendRs([Parent] Friendnotification n, [Service] gpplanetContext ctx)
        {
            return ctx.Friends.Where(b => b.FriendsId == n.FriendRsId).FirstOrDefault();
        }
    }
}
