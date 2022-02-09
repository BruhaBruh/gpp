using System.ComponentModel.DataAnnotations;
using static GPPlanetGQL.GraphQL.Scalars;
using static GPPlanetGQL.Models.Report;
using static GPPlanetGQL.Models.User;

namespace GPPlanetGQL.GraphQL
{
    public class Inputs
    {
        public class UserEditInput
        {
            public bool? IsBanned { get; set; }
            public string? Status { get; set; }
            public string? Description { get; set; }
            public int? Sex { get; set; }
            [GraphQLType(typeof(MediaLinkType))]
            public string? Avatar { get; set; }
            [GraphQLType(typeof(MediaLinkType))]
            public string? Banner { get; set; }
            public bool? IsShowPhone { get; set; }
            public bool? IsNotifyOnReport { get; set; }
            public bool? IsNotifyOnReportMessage { get; set; }
            public bool? IsNotifyOnNewSubscriber { get; set; }
            public bool? IsNotifyOnNewFriend { get; set; }
            public UserRoleEnum? role { get; set; }
            public long? permissions { get; set; }
        }

        public class ForumCreateInput
        {
            public string Name { get; set; }
            public int ParentForumId { get; set; }
            public string? Link { get; set; }
            public bool IsOpen { get; set; }
        }

        public class ForumEditInput
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public int? ParentForumId { get; set; }
            public string? Link { get; set; }
        }

        public class ThreadCreateInput
        {
            public string Name { get; set; }
            public int ForumId { get; set; }
            public bool IsPinned { get; set; }
            public bool CanChat { get; set; }
            public string Message { get; set; }
        }

        public class ThreadEditInput
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public bool? IsPinned { get; set; }
            public bool? CanChat { get; set; }
        }

        public class PostCreateInput
        {
            public int ThreadId { get; set; }
            public string Message { get; set; }
            public int? ReplyId { get; set; }
        }

        public class PostEditInput
        {
            public int Id { get; set; }
            public string Message { get; set; }
        }

        public class ReportCreateInput
        {
            public ReportType Type { get; set; }
            public ReportSubType Subtype { get; set; }
            public string Message { get; set; }
            public int? ReportTo { get; set; }
        }

        public class ReportSendMessageInput
        {
            public int ReportId { get; set; }
            public string Message { get; set; }
            public int? ReplyMessageId { get; set; }
        }
    }
}
