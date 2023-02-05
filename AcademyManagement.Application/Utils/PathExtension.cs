namespace AcademyManagement.Application.Utils
{
    public static class PathExtension
    {
        #region Domain Addresses

        public static string StaticFileEndPointURL = "https://localhost:44382";

        #endregion

         #region UserAvatar

        public static string DefaultUserAvatar = "/images/UserAvatar/Default/Default.jpg";

        public static string UserAvatarOrigin = StaticFileEndPointURL+"/images/UserAvatar/Origin/";
        public static string UserAvatarOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar/Origin/");

        public static string UserAvatarThumb = StaticFileEndPointURL+"/images/UserAvatar/Thumb/";
        public static string UserAvatarThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar/Thumb/");

        #endregion
    }
}
