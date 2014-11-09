namespace SubtitleCommunitySystem.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Web.Areas.Administration.Models;

    public class LanguagesController : AdminController
    {

        public LanguagesController(IApplicationData data) : base(data)
        {

        }

        // GET: Administration/Languages
        public ActionResult Index()
        {
            return View(this.Data.Languages.All()
                .OrderBy(l => l.Name)
                .Project().To<LanguageOutputModel>()
                .ToList());
        }

        // GET: Administration/Languages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var language = this.Data.Languages.All()
                .Where(l => l.Id == (int)id)
                .Project().To<LanguageOutputModel>().FirstOrDefault();

            if (language == null)
            {
                return HttpNotFound();
            }

            return View(language);
        }

        // GET: Administration/Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administration/Languages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] LanguageInputModel language)
        {
            if (ModelState.IsValid)
            {
                this.Data.Languages.Add(Mapper.Map<Language>(language));
                this.Data.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(language);
        }

        // GET: Administration/Languages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var language = this.Data.Languages.Find(id);
            if (language == null)
            {
                return HttpNotFound();
            }

            var inputLanguage = Mapper.Map<LanguageInputModel>(language);

            return View(inputLanguage);
        }

        // POST: Administration/Languages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] LanguageInputModel language)
        {
            if (ModelState.IsValid)
            {
                var dbLanguage = this.Data.Languages.Find(language.Id);

                if (dbLanguage == null)
                {
                    return HttpNotFound();                    
                }

                dbLanguage.Name = language.Name;

                this.Data.SaveChanges();

                return RedirectToAction("Edit");
            }

            return View(language);
        }

        // GET: Administration/Languages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Language language = this.Data.Languages.Find(id);

            if (language == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<LanguageInputModel>(language));
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

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
