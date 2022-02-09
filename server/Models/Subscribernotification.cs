using System;
using System.Collections.Generic;
using static GPPlanetGQL.GraphQL.Unions;

namespace GPPlanetGQL.Models
{
    public partial class Subscribernotification : INotification
    {
        public int SubscribernotificationId { get; set; }
        public int ToId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SubscriberRsId { get; set; }

        public virtual Subscriber SubscriberRs { get; set; } = null!;
        public virtual User To { get; set; } = null!;
        public Subscriber GetSubscriberRs([Parent] Subscribernotification n, [Service] gpplanetContext ctx)
        {
            return ctx.Subscribers.Where(b => b.SubscribersId == n.SubscriberRsId).FirstOrDefault();
        }
    }
}
