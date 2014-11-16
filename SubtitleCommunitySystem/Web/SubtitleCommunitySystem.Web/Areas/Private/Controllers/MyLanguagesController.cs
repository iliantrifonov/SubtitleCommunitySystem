﻿namespace SubtitleCommunitySystem.Web.Areas.Private.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Areas.Private.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;

    public class MyLanguagesController : AuthenticatedUserController
    {
        public MyLanguagesController(IApplicationData data)
            : base(data)
        {
        }

        // GET: Private/MyLanguages
        public ActionResult Index()
        {
            var languages = this.CurrentUser.Languages.OrderBy(l => l.Name)
                .AsQueryable()
                .Project().To<LanguageOutputModel>()
                .ToList();

            return this.View(languages);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var languages = this.Data.Languages.All().Select(l => new SelectListItem() { Text = l.Name, Value = l.Id.ToString() });

            var model = new AddLanguageViewModel()
            {
                Languages = languages
            };

            return this.View(model);
        }

        [HttpPost]
        public ActionResult Add(AddLanguageViewModel model)
        {
            var language = this.Data.Languages.Find(int.Parse(model.LanguageId));

            if (language == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var alreadyHasLanguage = this.CurrentUser.Languages.Any(l => l.Id == language.Id);

            if (alreadyHasLanguage)
            {
                return this.RedirectToAction("Index");
            }

            this.CurrentUser.Languages.Add(language);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}