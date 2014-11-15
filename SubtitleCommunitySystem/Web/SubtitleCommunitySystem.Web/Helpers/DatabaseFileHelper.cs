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
            if (!FileConstants.AllowedSourceExtentions.Contains(extention))
            {
                throw new ArgumentException("Incorrect file extention type.");
            }

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