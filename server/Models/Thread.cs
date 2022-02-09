using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPPlanetGQL.Models
{
    public partial class Thread
    {
        public Thread()
        {
            Posts = new HashSet<Post>();
        }

        public int ThreadId { get; set; }
        public int ForumId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPinned { get; set; }
        public bool? CanChat { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Forum Forum { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }

        [NotMapped]
        public virtual Post FirstPost { get; set; } = null!;

        [NotMapped]
        public virtual Post LastPost { get; set; } = null!;

        public Post? GetFirstPost([Parent] Thread thread, [Service] gpplanetContext ctx)
        {
            return ctx.Posts.Where(p => p.ThreadId == thread.ThreadId).OrderBy(p => p.CreatedAt).FirstOrDefault();
        }
        public Post? GetLastPost([Parent] Thread thread, [Service] gpplanetContext ctx)
        {
            return ctx.Posts.Where(p => p.ThreadId == thread.ThreadId).OrderByDescending(p => p.CreatedAt).FirstOrDefault();
        }
    }
}
