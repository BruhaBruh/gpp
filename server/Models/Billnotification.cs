using System;
using System.Collections.Generic;
using static GPPlanetGQL.GraphQL.Unions;

namespace GPPlanetGQL.Models
{
    public partial class Billnotification : INotification
    {
        public int BillnotificationId { get; set; }
        public int ToId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BillId { get; set; }

        public virtual Bill Bill { get; set; } = null!;
        public virtual User To { get; set; } = null!;

        public Bill GetBill([Parent] Billnotification n, [Service] gpplanetContext ctx)
        {
            return ctx.Bills.Where(b => b.BillId == n.BillId).FirstOrDefault();
        }
    }
}
