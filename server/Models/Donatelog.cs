using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Donatelog
    {
        public int DonatelogId { get; set; }
        public int BoughtItemId { get; set; }
        public int? LootItemId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }

        public virtual Donateitem BoughtItem { get; set; } = null!;
        public virtual Donateitem? LootItem { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
