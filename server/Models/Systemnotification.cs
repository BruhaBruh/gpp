using System;
using System.Collections.Generic;
using static GPPlanetGQL.GraphQL.Unions;

namespace GPPlanetGQL.Models
{
    public partial class Systemnotification : INotification
    {
        public int SystemnotificationId { get; set; }
        public int ToId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Message { get; set; } = null!;

        public virtual User To { get; set; } = null!;
    }
}
