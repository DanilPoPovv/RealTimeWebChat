namespace RealTimeWebChat.Helpers
{
    public static class AvatarTypeHelper
    {
        private static readonly HashSet<string> AllowedMimeTypes =
        [
            "image/jpeg",
        "image/png",
        "image/webp"
        ];

        private static readonly HashSet<string> AllowedExtensions =
        [
            ".jpg",
        ".jpeg",
        ".png",
        ".webp"
        ];

        public static bool IsAllowedMimeType(string contentType)
        {
            return AllowedMimeTypes.Contains(contentType);
        }

        public static bool IsAllowedExtensions(string fileExtension)
        {
            return AllowedExtensions.Contains(
                fileExtension.ToLowerInvariant()
            );
        }
    }
}
