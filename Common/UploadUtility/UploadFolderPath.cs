using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.UploadUtility
{
    public static class UploadFolderPath
    {
        private static string UploadFolder => "Upload";
        private static string UserUploadFolder => "User";
        public static string UserAvatarFolderUplaod => "Avatar";
        // Post Paths
        private static string PostUploadFolder => "Post";
        private static string PostUploadPoster => "Poster";
        private static string WebRootPath => Directory.GetCurrentDirectory();
        /// <summary>
        /// Implements Paths
        /// </summary>
        /// <returns></returns>
        public static string PathUploadFolder()
        {
            return Path.Combine(WebRootPath, UploadFolder);
        }
        public static string PathUserUploadFolder()
        {
            return Path.Combine(WebRootPath, UploadFolder, UserUploadFolder);
        }
        public static string PathAvatarUserUploadFolder()
        {
            return Path.Combine(WebRootPath, UploadFolder, UserUploadFolder, UserAvatarFolderUplaod);
        }
        // Poster Patch Combain
        public static string PathPosterUploadFolder()
        {
            return Path.Combine(WebRootPath, UploadFolder, PostUploadFolder);
        }
        public static string PathPosterUpload()
        {
            return Path.Combine(WebRootPath, UploadFolder, PostUploadFolder, PostUploadPoster);
        }
    }
}
