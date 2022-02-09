using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Loottable
    {
        public int LoottableId { get; set; }
        public int CaseId { get; set; }
        public int ItemId { get; set; }
        public int Weight { get; set; }

        public virtual Donateitem Case { get; set; } = null!;
        public virtual Donateitem Item { get; set; } = null!;

        public static int? getRandomItemId(Loottable[] items)
        {
            var totalWeigth = 0;
            foreach (var item in items)
            {
                totalWeigth += item.Weight;
            }
            var randomNumber = new Random().Next(totalWeigth) + 1;

            var weight = 0;

            var sortedItems = items.OrderBy(i => i.Weight).ToArray();

            foreach (var item in sortedItems)
            {
                if (item.Weight <= 0) continue;
                weight += item.Weight;
                if (randomNumber <= weight)
                {
                    return item.ItemId;
                }
            }
            return null;
        }
    }
}
