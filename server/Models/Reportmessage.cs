using FluentValidation;
using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Reportmessage
    {
        public Reportmessage()
        {
            InverseReplymessage = new HashSet<Reportmessage>();
        }

        public int ReportmessageId { get; set; }
        public int ReportId { get; set; }
        public int OwnerId { get; set; }
        public string Message { get; set; } = null!;
        public int? ReplymessageId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual User Owner { get; set; } = null!;
        public virtual Reportmessage? Replymessage { get; set; }
        public virtual Report Report { get; set; } = null!;
        public virtual ICollection<Reportmessage> InverseReplymessage { get; set; }

        public class ReportmessageValidator : AbstractValidator<Reportmessage>
        {
            public ReportmessageValidator()
            {
                RuleFor(m => m.Message).MinimumLength(1).MaximumLength(1000);
            }
        }

        public User GetOwner([Parent] Reportmessage msg, [Service] gpplanetContext ctx)
        {
            return ctx.Users.Where(u => u.UserId == msg.OwnerId).FirstOrDefault();
        }

        public Reportmessage? GetReplymessage([Parent] Reportmessage m, [Service] gpplanetContext ctx)
        {
            return ctx.Reportmessages.Where(rms => rms.ReportmessageId == m.ReplymessageId).FirstOrDefault();
        }
    }
}
