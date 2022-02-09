using GPPlanetGQL.Variables;
using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Donateitem
    {
        public enum DonateTypes
        {
            Premium,
            Lite,
            Trefs,
            Case,
            Money
        }

        public enum ItemIcon
        {
            Premium,
            Lite,
            Trefs,
            Case,
            Money
        }

        public Donateitem()
        {
            DonatelogBoughtItems = new HashSet<Donatelog>();
            DonatelogLootItems = new HashSet<Donatelog>();
            LoottableCases = new HashSet<Loottable>();
            LoottableItems = new HashSet<Loottable>();
        }

        public int DonateitemId { get; set; }
        public string Name { get; set; } = null!;
        public int? Icon { get; set; }
        public int Cost { get; set; }
        public int Amount { get; set; }
        public bool IsShow { get; set; }
        public int Type { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Donatelog> DonatelogBoughtItems { get; set; }
        public virtual ICollection<Donatelog> DonatelogLootItems { get; set; }
        public virtual ICollection<Loottable> LoottableCases { get; set; }
        public virtual ICollection<Loottable> LoottableItems { get; set; }
    }
}
