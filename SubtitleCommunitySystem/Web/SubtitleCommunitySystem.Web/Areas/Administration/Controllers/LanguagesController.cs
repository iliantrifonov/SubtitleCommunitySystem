namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;
    using SubtitleCommunitySystem.Web.Controllers.Base;

    public class LanguagesController : AdminController
    {
        public LanguagesController(IApplicationData data) : base(data)
        {
        }

        // GET: Administration/Languages
        public ActionResult Index([DataSourceRequest]
                                  DataSourceRequest request)
        {
            return this.View(this.Data.Languages.All().Project().To<LanguageOutputModel>().ToDataSourceResult(request).Data);
        }

        // GET: Administration/Languages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            var language = this.Data.Languages.All()
                               .Where(l => l.Id == (int)id)
                               .Project().To<LanguageOutputModel>().FirstOrDefault();

            if (language == null)
            {
                return this.HttpNotFound();
            }

            return this.View(language);
        }

        // GET: Administration/Languages/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")]
                                   LanguageInputModel language)
        {
            if (this.ModelState.IsValid)
            {
                this.Data.Languages.Add(Mapper.Map<Language>(language));
                this.Data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(language);
        }

        // GET: Administration/Languages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }
            var language = this.Data.Languages.Find(id);
            if (language == null)
            {
                return this.HttpNotFound();
            }

            var inputLanguage = Mapper.Map<LanguageInputModel>(language);

            return this.View(inputLanguage);
        }

        // POST: Administration/Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")]
                                 LanguageInputModel language)
        {
            if (this.ModelState.IsValid)
            {
                var dbLanguage = this.Data.Languages.Find(language.Id);

                if (dbLanguage == null)
                {
                    return this.HttpNotFound();                    
                }

                dbLanguage.Name = language.Name;

                this.Data.SaveChanges();
                this.TempData["Success"] = "Language is updated!";

                return this.RedirectToAction("Edit");
            }

            return this.View(language);
        }

        // GET: Administration/Languages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new HttpException(404, "Incorrect input data!");
            }

            Language language = this.Data.Languages.Find(id);

            if (language == null)
            {
                return this.HttpNotFound();
            }

            return this.View(Mapper.Map<LanguageInputModel>(language));
        }

        // POST: Administration/Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Language language = this.Data.Languages.Find(id);

            language.Subtitles.Clear();
            language.Teams.Clear();

            this.Data.Languages.Delete(language);
            this.Data.SaveChanges();

            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
