using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Bill
    {
        public Bill()
        {
            Billnotifications = new HashSet<Billnotification>();
        }
        public int BillId { get; set; }
        public int Amount { get; set; }
        public string Status { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Billnotification> Billnotifications { get; set; }
    }
}
