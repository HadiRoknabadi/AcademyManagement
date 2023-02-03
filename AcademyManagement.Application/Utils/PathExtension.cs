namespace AcademyManagement.Infrastructure.Utils
{
    public static class PathExtension
    {
        #region Domain Addresses

        public static string StaticFileUploaderURL = "https://localhost:44382";

        #endregion

         #region UserAvatar

        public static string DefaultUserAvatar = "/images/UserAvatar/Default/Default.jpg";

        public static string UserAvatarOrigin = "/images/UserAvatar/Origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar/Origin/");

        public static string UserAvatarThumb = "/images/UserAvatar/Thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar/Thumb/");

        #endregion
    }
}
