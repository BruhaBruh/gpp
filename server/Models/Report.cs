using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPPlanetGQL.Models
{
    public partial class Report
    {
        public enum ReportType
        {
            Report,
            Bug,
            Feature
        }

        public enum ReportSubType
        {
            Admin,
            User,
            Server,
            Site
        }

        public Report()
        {
            Reportmessages = new HashSet<Reportmessage>();
        }

        public int ReportId { get; set; }
        public ReportType Type { get; set; }
        public ReportSubType Subtype { get; set; }
        public int OwnerId { get; set; }
        public int? ToId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsClosed { get; set; }
        [NotMapped]
        public Reportmessage? LastMessage { get; set; } = null;

        public virtual User Owner { get; set; } = null!;
        public virtual User? To { get; set; }
        public virtual ICollection<Reportmessage> Reportmessages { get; set; }
        public Reportmessage? GetLastMessage([Parent] Report report, [Service] gpplanetContext ctx)
        {
            return ctx.Reportmessages.Where(rms => rms.ReportId == report.ReportId).OrderByDescending(rms => rms.CreatedAt).FirstOrDefault();
        }
    }
}
