using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Subscriber
    {
        public Subscriber()
        {
            Subscribernotifications = new HashSet<Subscribernotification>();
        }

        public int SubscribersId { get; set; }
        public int UserId { get; set; }
        public int SubscriberId { get; set; }

        public virtual User SubscriberNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Subscribernotification> Subscribernotifications { get; set; }
        public User GetSubscriberNavigation([Parent] Subscriber n, [Service] gpplanetContext ctx)
        {
            return ctx.Users.Where(b => b.UserId == n.SubscriberId).FirstOrDefault();
        }
    }
}
