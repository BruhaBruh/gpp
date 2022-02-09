using FluentValidation;

using GPPlanetGQL.Discord;
using GPPlanetGQL.Exceptions;
using GPPlanetGQL.Services;
using GPPlanetGQL.Variables;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

using static GPPlanetGQL.GraphQL.Scalars;

namespace GPPlanetGQL.Models
{
    public partial class User
    {
        public enum UserRoleEnum
        {
            None,
            SiteDeveloper,
            Admin,
            Moderator,
            JrModerator,
        }

        public enum UserTopEnum
        {
            Views,
            Rating,
            Friends,
            Subscribers,
            Years,
            RatingN,
            SocialPoints,
            SocialPointsN
        }

        public User()
        {
            Billnotifications = new HashSet<Billnotification>();
            Bills = new HashSet<Bill>();
            Donatelogs = new HashSet<Donatelog>();
            FriendFriendNavigations = new HashSet<Friend>();
            FriendUsers = new HashSet<Friend>();
            Friendnotifications = new HashSet<Friendnotification>();
            Posts = new HashSet<Post>();
            RatingFroms = new HashSet<Rating>();
            RatingTos = new HashSet<Rating>();
            ReportOwners = new HashSet<Report>();
            ReportTos = new HashSet<Report>();
            Reportmessages = new HashSet<Reportmessage>();
            SubscriberSubscriberNavigations = new HashSet<Subscriber>();
            SubscriberUsers = new HashSet<Subscriber>();
            Subscribernotifications = new HashSet<Subscribernotification>();
            Systemnotifications = new HashSet<Systemnotification>();
        }

        public int UserId { get; set; }
        public long DiscordId { get; set; }
        public int Money { get; set; }
        public bool IsBanned { get; set; }
        [GraphQLType(typeof(MediaLinkType))]
        public string Avatar { get; set; } = null!;
        [GraphQLType(typeof(MediaLinkType))]
        public string Banner { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Views { get; set; }
        public int Sex { get; set; }
        public long Settings { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public DateTime LastOnline { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long Permissions { get; set; }
        public DateTime? SubscriptionEndAt { get; set; }
        public DateTime? BanreportEndAt { get; set; }
        public int SocialPoints { get; set; }

        public virtual ICollection<Billnotification> Billnotifications { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Donatelog> Donatelogs { get; set; }
        public virtual ICollection<Friend> FriendFriendNavigations { get; set; }
        public virtual ICollection<Friend> FriendUsers { get; set; }
        public virtual ICollection<Friendnotification> Friendnotifications { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Rating> RatingFroms { get; set; }
        public virtual ICollection<Rating> RatingTos { get; set; }
        public virtual ICollection<Reportmessage> Reportmessages { get; set; }
        public virtual ICollection<Report> ReportOwners { get; set; }
        public virtual ICollection<Report> ReportTos { get; set; }
        public virtual ICollection<Subscriber> SubscriberSubscriberNavigations { get; set; }
        public virtual ICollection<Subscriber> SubscriberUsers { get; set; }
        public virtual ICollection<Subscribernotification> Subscribernotifications { get; set; }
        public virtual ICollection<Systemnotification> Systemnotifications { get; set; }
        [NotMapped]
        public string Nickname { get; set; } = "";
        [NotMapped]
        public string Tag { get; set; } = "";
        [NotMapped]
        public string? Work { get; set; } = null;
        [NotMapped]
        public string? Role { get; set; } = null;
        [NotMapped]
        public int Level { get; set; } = 0;
        [NotMapped]
        public int? Phone { get; set; } = null;
        [NotMapped]
        public int? Trefs { get; set; } = 0;
        [NotMapped]
        public int? TotalFriends { get; set; } = 0;
        [NotMapped]
        public int? TotalSubscribers { get; set; } = 0;
        [NotMapped]
        public UserRating Rating { get; set; } = new UserRating
        {
            Total = 0,
            Result = 0,
            Positive = 0,
            Negative = 0,
            Your = 0,
        };
        [NotMapped]
        public UserDiscordRole[] DiscordRoles { get; set; } = Array.Empty<UserDiscordRole>();

        public string GetNickname([Parent] User user, [Service] DiscordBot bot)
        {
            return bot.GetNicknameByDiscordId((ulong)user.DiscordId);
        }

        public string GetTag([Parent] User user, [Service] DiscordBot bot)
        {
            return bot.GetTagByDiscordId((ulong)user.DiscordId);
        }

        public string GetAvatar([Parent] User user, [Service] DiscordBot bot, [Service] gpplanetContext ctx)
        {
            var avatar = ctx.Users.Where(u => u.UserId == user.UserId).Select(u => u.Avatar).FirstOrDefault();
            if (avatar == null) return bot.GetAvatarByDiscordId((ulong)user.DiscordId);
            if (avatar.Trim().Length == 0) return bot.GetAvatarByDiscordId((ulong)user.DiscordId);
            return avatar;
        }

        public async Task<string?> GetWork([Parent] User user, [Service] LimeService lime)
        {
            if (user.DiscordId == 0) return null;
            return (await lime.GetUserData(user.DiscordId)).work;
        }

        public async Task<string?> GetRole([Parent] User user, [Service] LimeService lime)
        {
            if (user.DiscordId == 0) return null;
            return (await lime.GetUserData(user.DiscordId)).role;
        }

        public async Task<int> GetLevel([Parent] User user, [Service] LimeService lime)
        {
            if (user.DiscordId == 0) return 0;
            return (await lime.GetUserData(user.DiscordId)).level;
        }

        public async Task<int?> GetPhone([Parent] User user, [Service] LimeService lime)
        {
            if (user.DiscordId == 0 || (user.Settings & UserSettings.ShowPhone) == 0) return null;
            return (await lime.GetUserData(user.DiscordId)).phone;
        }

        public UserDiscordRole[] GetDiscordRoles([Parent] User user, [Service] DiscordBot bot)
        {
            if (user.DiscordId == 0) return Array.Empty<UserDiscordRole>(); ;
            return bot.GetDiscordRolesByDiscordId((ulong)user.DiscordId);
        }

        public int GetTotalFriends([Parent] User user,
                [Service] gpplanetContext ctx)
        {
            return ctx.Friends.Where(s => s.UserId == user.UserId).Count();
        }

        public int GetTotalSubscribers([Parent] User user,
                [Service] gpplanetContext ctx)
        {
            return ctx.Subscribers.Where(s => s.UserId == user.UserId).Count();
        }

        public IQueryable<Bill> GetBills([Parent] User user,
                [Service] gpplanetContext ctx)
        {
            return ctx.Bills.Where(b => b.UserId == user.UserId);
        }

        public IQueryable<Rating> GetRatingTos([Parent] User user,
                [Service] gpplanetContext ctx)
        {
            return ctx.Ratings.Where(r => r.ToId == user.UserId);
        }

        public UserRating GetRating(
                [Parent] User user,
                [Service] gpplanetContext ctx,
                [GlobalState(nameof(ClaimsPrincipal))] ClaimsPrincipal claimsPrincipal
            )
        {
            var discordId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var positiveRatings = ctx.Ratings.Count(r => r.ToId == user.UserId && r.Positive);
            var negativeRatings = ctx.Ratings.Count(r => r.ToId == user.UserId && !r.Positive);
            if (discordId == null)
            {
                return new UserRating
                {
                    Total = positiveRatings + negativeRatings,
                    Result = positiveRatings - negativeRatings,
                    Positive = positiveRatings,
                    Negative = negativeRatings,
                    Your = 0,
                };
            } else
            {
                var your = ctx.Ratings.Where(r => r.ToId == user.UserId && r.From.DiscordId == long.Parse(discordId)).FirstOrDefault();
                return new UserRating
                {
                    Total = positiveRatings + negativeRatings,
                    Result = positiveRatings - negativeRatings,
                    Positive = positiveRatings,
                    Negative = negativeRatings,
                    Your = your == null ? 0 : (your.Positive ? 1 : -1),
                };
            }
        }

        public class UserRating
        {
            public int Total { get; set; }
            public int Result { get; set; }
            public int Positive { get; set; }
            public int Negative { get; set; }
            public int Your { get; set; }
        }

        public class UserDiscordRole
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public int Position { get; set; }
            public string Color { get; set; }
            public bool Hoist { get; set; }
        }

        public class UserValidator : AbstractValidator<User>
        {
            public UserValidator()
            {
                RuleFor(u => u.Money).LessThanOrEqualTo(10000000).GreaterThanOrEqualTo(0);
                RuleFor(u => u.DiscordId).NotEmpty();
                RuleFor(u => u.Avatar).MaximumLength(128);
                RuleFor(u => u.Banner).MaximumLength(128);
                RuleFor(u => u.Status).MaximumLength(128);
                RuleFor(u => u.Description).MaximumLength(6000);
                RuleFor(u => u.Views).GreaterThanOrEqualTo(0);
                RuleFor(u => u.Sex).GreaterThanOrEqualTo(0).LessThanOrEqualTo(2);
            }
        }

        public class UserStatus
        {
            public bool IsFriends { get; set; }
            public bool HeIsSubscriber { get; set; }
            public bool YouIsSubscriber { get; set; }
        }

        public static UserStatus GetUserStatus(int yourId, int himId, gpplanetContext ctx)
        {
            var heSubscriber = ctx.Subscribers.Where(s => s.UserId == yourId && s.SubscriberId == himId).FirstOrDefault();
            var youSubscriber = ctx.Subscribers.Where(s => s.UserId == himId && s.SubscriberId == yourId).FirstOrDefault();
            var isFriends = ctx.Friends.Where(s => s.UserId == yourId && s.FriendId == himId).FirstOrDefault();
            return new UserStatus
            {
                IsFriends = isFriends != null,
                HeIsSubscriber = heSubscriber != null,
                YouIsSubscriber = youSubscriber != null,
            };
        }
    }
}
