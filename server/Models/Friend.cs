using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Friend
    {
        public Friend()
        {
            Friendnotifications = new HashSet<Friendnotification>();
        }

        public int FriendsId { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public virtual User FriendNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Friendnotification> Friendnotifications { get; set; }
        public User GetFriendNavigation([Parent] Friend n, [Service] gpplanetContext ctx)
        {
            return ctx.Users.Where(b => b.UserId == n.FriendId).FirstOrDefault();
        }
    }
}
