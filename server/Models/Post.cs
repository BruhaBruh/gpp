using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int ThreadId { get; set; }
        public int OwnerId { get; set; }
        public string Message { get; set; } = null!;
        public int? ReplyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User Owner { get; set; } = null!;
        public virtual Post? Reply { get; set; }
        public virtual Thread Thread { get; set; } = null!;
        public virtual ICollection<Post> InverseReply { get; set; }

        public User? GetOwner([Parent] Post post, [Service] gpplanetContext ctx)
        {
            return ctx.Users.Where(u => u.UserId == post.OwnerId).FirstOrDefault();
        }

        public Post? GetReply([Parent] Post post, [Service] gpplanetContext ctx)
        {
            if (post.ReplyId == null) return null;
            return ctx.Posts.Where(p => p.PostId == post.ReplyId).FirstOrDefault();
        }
    }
}
