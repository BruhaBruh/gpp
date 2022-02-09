using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public bool Positive { get; set; }

        public virtual User From { get; set; } = null!;
        public virtual User To { get; set; } = null!;


        public User GetFrom([Parent] Rating rating,
                [Service] gpplanetContext ctx)
        {
            return ctx.Users.Where(u => u.UserId == rating.FromId).FirstOrDefault();
        }
    }
}
