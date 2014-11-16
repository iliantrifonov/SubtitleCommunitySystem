namespace SubtitleCommunitySystem.Web.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;

    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Infrastructure.Constants;

    public class UploadHelper
    {
        public static string UploadPictureToServer(HttpPostedFileBase file, string fileName, MovieInputModel movie)
        {
            var extention = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            if (!FileConstants.AllowedPictureExtentions.Contains(extention))
            {
                throw new ArgumentException("Incorrect file extention type.");
            }

            var directoryName = "~/Files/" + movie.Name;
            if (string.IsNullOrWhiteSpace(movie.Directory))
            {
                if (Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/" + movie.Name)))
                {
                    var i = 0;
                    while (Directory.Exists(HttpContext.Current.Server.MapPath("~/Files/" + movie.Name + "." + i)))
                    {
                        i++;
                    }

                    directoryName = "~/Files/" + movie.Name + "." + i;
                }
            }
            else
            {
                directoryName = movie.Directory;
            }

            movie.Directory = directoryName;
            var mappedDirectoryName = HttpContext.Current.Server.MapPath(directoryName);
            Directory.CreateDirectory(mappedDirectoryName);

            file.SaveAs(mappedDirectoryName + "/" + fileName + extention);

            return directoryName.Substring(1) + "/" + fileName + extention;
        }
    }
}