namespace GPPlanetGQL.Variables
{
    public class Permissions
    {
        public static readonly long All = 1;
        public static readonly long GlobalChatRemove = 2;
        public static readonly long ModifyPermissions = 4;
        public static readonly long SetBan = 8;
        public static readonly long RemoveBan = 16;
        public static readonly long ShowReports = 32;
        public static readonly long ModifyRoles = 64;
        public static readonly long Lite = 128;
        public static readonly long Premium = 256;
        public static readonly long ModifyForum = 512;
        public static readonly long ModifyThread = 1024;
        public static readonly long ModifySocialPoints = 2048;
    }
}
