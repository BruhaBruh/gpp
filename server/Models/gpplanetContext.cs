using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GPPlanetGQL.Models
{
    public partial class gpplanetContext : DbContext
    {
        public gpplanetContext()
        {
        }

        public gpplanetContext(DbContextOptions<gpplanetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Billnotification> Billnotifications { get; set; } = null!;
        public virtual DbSet<Donateitem> Donateitems { get; set; } = null!;
        public virtual DbSet<Donatelog> Donatelogs { get; set; } = null!;
        public virtual DbSet<Forum> Forums { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Friendnotification> Friendnotifications { get; set; } = null!;
        public virtual DbSet<Loottable> Loottables { get; set; } = null!;
        public virtual DbSet<Newplayerslog> Newplayerslogs { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Reportmessage> Reportmessages { get; set; } = null!;
        public virtual DbSet<Serveronlinelog> Serveronlinelogs { get; set; } = null!;
        public virtual DbSet<Siteonlinelog> Siteonlinelogs { get; set; } = null!;
        public virtual DbSet<Subscriber> Subscribers { get; set; } = null!;
        public virtual DbSet<Subscribernotification> Subscribernotifications { get; set; } = null!;
        public virtual DbSet<Systemnotification> Systemnotifications { get; set; } = null!;
        public virtual DbSet<Thread> Threads { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=130.61.216.16;Database=gpplanet;Username=admin;Password=ZKEVL2P4x89fwe5r");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("bills");

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CompletedAt).HasColumnName("completed_at");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Status)
                    .HasMaxLength(12)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'WAITING'::character varying");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("bills_user_id_fkey");
            });

            modelBuilder.Entity<Billnotification>(entity =>
            {
                entity.ToTable("billnotifications");

                entity.Property(e => e.BillnotificationId).HasColumnName("billnotification_id");

                entity.Property(e => e.BillId).HasColumnName("bill_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ToId).HasColumnName("to_id");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.Billnotifications)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("billnotifications_bill_id_fkey");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.Billnotifications)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("billnotifications_to_id_fkey");
            });

            modelBuilder.Entity<Donateitem>(entity =>
            {
                entity.ToTable("donateitems");

                entity.Property(e => e.DonateitemId).HasColumnName("donateitem_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Description)
                    .HasMaxLength(256)
                    .HasColumnName("description")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Icon).HasColumnName("icon");

                entity.Property(e => e.IsShow).HasColumnName("is_show");

                entity.Property(e => e.Name)
                    .HasMaxLength(32)
                    .HasColumnName("name");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Donatelog>(entity =>
            {
                entity.ToTable("donatelogs");

                entity.Property(e => e.DonatelogId).HasColumnName("donatelog_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.BoughtItemId).HasColumnName("bought_item_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.LootItemId).HasColumnName("loot_item_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.BoughtItem)
                    .WithMany(p => p.DonatelogBoughtItems)
                    .HasForeignKey(d => d.BoughtItemId)
                    .HasConstraintName("donatelogs_bought_item_id_fkey");

                entity.HasOne(d => d.LootItem)
                    .WithMany(p => p.DonatelogLootItems)
                    .HasForeignKey(d => d.LootItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("donatelogs_loot_item_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Donatelogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("donatelogs_user_id_fkey");
            });

            modelBuilder.Entity<Forum>(entity =>
            {
                entity.ToTable("forums");

                entity.Property(e => e.ForumId).HasColumnName("forum_id");

                entity.Property(e => e.IsOpen).HasColumnName("is_open");

                entity.Property(e => e.Link)
                    .HasMaxLength(256)
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ParentForumId).HasColumnName("parent_forum_id");

                entity.HasOne(d => d.ParentForum)
                    .WithMany(p => p.InverseParentForum)
                    .HasForeignKey(d => d.ParentForumId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("forums_parent_forum_id_fkey");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.FriendsId)
                    .HasName("friends_pkey");

                entity.ToTable("friends");

                entity.Property(e => e.FriendsId).HasColumnName("friends_id");

                entity.Property(e => e.FriendId).HasColumnName("friend_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.FriendNavigation)
                    .WithMany(p => p.FriendFriendNavigations)
                    .HasForeignKey(d => d.FriendId)
                    .HasConstraintName("friends_friend_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FriendUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("friends_user_id_fkey");
            });

            modelBuilder.Entity<Friendnotification>(entity =>
            {
                entity.ToTable("friendnotifications");

                entity.Property(e => e.FriendnotificationId).HasColumnName("friendnotification_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.FriendRsId).HasColumnName("friend_rs_id");

                entity.Property(e => e.ToId).HasColumnName("to_id");

                entity.HasOne(d => d.FriendRs)
                    .WithMany(p => p.Friendnotifications)
                    .HasForeignKey(d => d.FriendRsId)
                    .HasConstraintName("friendnotifications_friend_rs_id_fkey");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.Friendnotifications)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("friendnotifications_to_id_fkey");
            });

            modelBuilder.Entity<Loottable>(entity =>
            {
                entity.ToTable("loottables");

                entity.Property(e => e.LoottableId).HasColumnName("loottable_id");

                entity.Property(e => e.CaseId).HasColumnName("case_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.LoottableCases)
                    .HasForeignKey(d => d.CaseId)
                    .HasConstraintName("loottables_case_id_fkey");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.LoottableItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("loottables_item_id_fkey");
            });

            modelBuilder.Entity<Newplayerslog>(entity =>
            {
                entity.ToTable("newplayerslogs");

                entity.Property(e => e.NewplayerslogId).HasColumnName("newplayerslog_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Total).HasColumnName("total");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Message).HasColumnName("message");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.ReplyId).HasColumnName("reply_id");

                entity.Property(e => e.ThreadId).HasColumnName("thread_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("posts_owner_id_fkey");

                entity.HasOne(d => d.Reply)
                    .WithMany(p => p.InverseReply)
                    .HasForeignKey(d => d.ReplyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("posts_reply_id_fkey");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ThreadId)
                    .HasConstraintName("posts_thread_id_fkey");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.ToTable("ratings");

                entity.Property(e => e.RatingId).HasColumnName("rating_id");

                entity.Property(e => e.FromId).HasColumnName("from_id");

                entity.Property(e => e.Positive).HasColumnName("positive");

                entity.Property(e => e.ToId).HasColumnName("to_id");

                entity.HasOne(d => d.From)
                    .WithMany(p => p.RatingFroms)
                    .HasForeignKey(d => d.FromId)
                    .HasConstraintName("ratings_from_id_fkey");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.RatingTos)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("ratings_to_id_fkey");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("reports");

                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.IsClosed).HasColumnName("is_closed");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.Subtype).HasColumnName("subtype");

                entity.Property(e => e.ToId).HasColumnName("to_id");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.ReportOwners)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("reports_owner_id_fkey");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.ReportTos)
                    .HasForeignKey(d => d.ToId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("reports_to_id_fkey");
            });

            modelBuilder.Entity<Reportmessage>(entity =>
            {
                entity.ToTable("reportmessages");

                entity.Property(e => e.ReportmessageId).HasColumnName("reportmessage_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Message)
                    .HasMaxLength(1000)
                    .HasColumnName("message");

                entity.Property(e => e.OwnerId).HasColumnName("owner_id");

                entity.Property(e => e.ReplymessageId).HasColumnName("replymessage_id");

                entity.Property(e => e.ReportId).HasColumnName("report_id");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Reportmessages)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("reportmessages_owner_id_fkey");

                entity.HasOne(d => d.Replymessage)
                    .WithMany(p => p.InverseReplymessage)
                    .HasForeignKey(d => d.ReplymessageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("reportmessages_replymessage_id_fkey");

                entity.HasOne(d => d.Report)
                    .WithMany(p => p.Reportmessages)
                    .HasForeignKey(d => d.ReportId)
                    .HasConstraintName("reportmessages_report_id_fkey");
            });

            modelBuilder.Entity<Serveronlinelog>(entity =>
            {
                entity.ToTable("serveronlinelogs");

                entity.Property(e => e.ServeronlinelogId).HasColumnName("serveronlinelog_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Online).HasColumnName("online");
            });

            modelBuilder.Entity<Siteonlinelog>(entity =>
            {
                entity.ToTable("siteonlinelogs");

                entity.Property(e => e.SiteonlinelogId).HasColumnName("siteonlinelog_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Online).HasColumnName("online");
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.HasKey(e => e.SubscribersId)
                    .HasName("subscribers_pkey");

                entity.ToTable("subscribers");

                entity.Property(e => e.SubscribersId).HasColumnName("subscribers_id");

                entity.Property(e => e.SubscriberId).HasColumnName("subscriber_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.SubscriberNavigation)
                    .WithMany(p => p.SubscriberSubscriberNavigations)
                    .HasForeignKey(d => d.SubscriberId)
                    .HasConstraintName("subscribers_subscriber_id_fkey");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SubscriberUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("subscribers_user_id_fkey");
            });

            modelBuilder.Entity<Subscribernotification>(entity =>
            {
                entity.ToTable("subscribernotifications");

                entity.Property(e => e.SubscribernotificationId).HasColumnName("subscribernotification_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.SubscriberRsId).HasColumnName("subscriber_rs_id");

                entity.Property(e => e.ToId).HasColumnName("to_id");

                entity.HasOne(d => d.SubscriberRs)
                    .WithMany(p => p.Subscribernotifications)
                    .HasForeignKey(d => d.SubscriberRsId)
                    .HasConstraintName("subscribernotifications_subscriber_rs_id_fkey");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.Subscribernotifications)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("subscribernotifications_to_id_fkey");
            });

            modelBuilder.Entity<Systemnotification>(entity =>
            {
                entity.ToTable("systemnotifications");

                entity.Property(e => e.SystemnotificationId).HasColumnName("systemnotification_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Message)
                    .HasMaxLength(128)
                    .HasColumnName("message");

                entity.Property(e => e.ToId).HasColumnName("to_id");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.Systemnotifications)
                    .HasForeignKey(d => d.ToId)
                    .HasConstraintName("systemnotifications_to_id_fkey");
            });

            modelBuilder.Entity<Thread>(entity =>
            {
                entity.ToTable("threads");

                entity.Property(e => e.ThreadId).HasColumnName("thread_id");

                entity.Property(e => e.CanChat)
                    .IsRequired()
                    .HasColumnName("can_chat")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.ForumId).HasColumnName("forum_id");

                entity.Property(e => e.IsPinned).HasColumnName("is_pinned");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.HasOne(d => d.Forum)
                    .WithMany(p => p.Threads)
                    .HasForeignKey(d => d.ForumId)
                    .HasConstraintName("threads_forum_id_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.DiscordId, "users_discord_id_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(128)
                    .HasColumnName("avatar")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Banner)
                    .HasMaxLength(128)
                    .HasColumnName("banner")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.BanreportEndAt).HasColumnName("banreport_end_at");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .HasMaxLength(6000)
                    .HasColumnName("description")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.DiscordId).HasColumnName("discord_id");

                entity.Property(e => e.IsBanned).HasColumnName("is_banned");

                entity.Property(e => e.LastOnline)
                    .HasColumnName("last_online")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Money).HasColumnName("money");

                entity.Property(e => e.Permissions).HasColumnName("permissions");

                entity.Property(e => e.Settings)
                    .HasColumnName("settings")
                    .HasDefaultValueSql("2");

                entity.Property(e => e.Sex).HasColumnName("sex");

                entity.Property(e => e.SocialPoints)
                    .HasColumnName("social_points")
                    .HasDefaultValueSql("100");

                entity.Property(e => e.Status)
                    .HasMaxLength(128)
                    .HasColumnName("status")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.SubscriptionEndAt).HasColumnName("subscription_end_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UserRole).HasColumnName("user_role");

                entity.Property(e => e.Views).HasColumnName("views");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
