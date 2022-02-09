using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Serveronlinelog
    {
        public int ServeronlinelogId { get; set; }
        public int Online { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
