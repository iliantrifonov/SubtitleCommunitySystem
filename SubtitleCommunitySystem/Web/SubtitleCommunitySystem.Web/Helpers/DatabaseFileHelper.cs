namespace SubtitleCommunitySystem.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class DatabaseFileHelper
    {
        public static DbFile GetDbFile(HttpPostedFileBase file, string fileName)
        {
            var extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            if (!FileConstants.AllowedNonSubtitleExtentions.Contains(extention))
            {
                throw new ArgumentException("Incorrect file extention type.");
            }

            return CreateDbFile(file, fileName, extention);
        }

        public static DbFile GetSubtitleDbFile(HttpPostedFileBase file, string fileName)
        {
            var extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            if (!FileConstants.AllowedSubtitleExtentions.Contains(extention))
            {
                throw new ArgumentException("Incorrect file extention type.");
            }

            return CreateDbFile(file, fileName, extention);
        }

        private static DbFile CreateDbFile(HttpPostedFileBase file, string fileName, string extention)
        {
            var dbFile = new DbFile()
            {
                ContentType = file.ContentType,
                FileName = fileName + extention,
            };

            using (var memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                dbFile.Content = memoryStream.ToArray();
            }

            return dbFile;
        }
    }
}