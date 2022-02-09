using System;
using System.Collections.Generic;

namespace GPPlanetGQL.Models
{
    public partial class Forum
    {
        public Forum()
        {
            InverseParentForum = new HashSet<Forum>();
            Threads = new HashSet<Thread>();
        }

        public int ForumId { get; set; }
        public int? ParentForumId { get; set; }
        public string Name { get; set; } = null!;
        public string? Link { get; set; }
        public bool IsOpen { get; set; }

        public virtual Forum? ParentForum { get; set; }
        public virtual ICollection<Forum> InverseParentForum { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
    }
}
