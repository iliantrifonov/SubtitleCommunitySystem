﻿namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using SubtitleCommunitySystem.Web.Controllers.Base;
    using SubtitleCommunitySystem.Data;

    public class FileController : AuthenticatedUserController
    {
        public FileController(IApplicationData data)
            : base(data)
        {

        }

        [HttpGet]
        public ActionResult DownloadSource(int id)
        {
            var file = this.Data.Files.Find(id);

            if (file == null)
            {
                throw new HttpException(404, "Could not file your file");
            }

            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + file.FileName + "\"");
            return File(file.Content, file.ContentType);
        }
    }
}