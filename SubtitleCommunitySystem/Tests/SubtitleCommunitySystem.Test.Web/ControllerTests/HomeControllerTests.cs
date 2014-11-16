namespace SubtitleCommunitySystem.Test.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Data.Repositories;
    using SubtitleCommunitySystem.Model;
    using SubtitleCommunitySystem.Web;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Web.ViewModels;

    [TestClass]
    public class HomeControllerTests
    {
        public HomeControllerTests()
        {
            MvcApplication.ConfigureAutoMapper();
        }

        [TestMethod]
        public void Index_Always_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_Always_ReturnsViewWithNullModel()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Index() as ViewResult;

            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void About_Always_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_Always_ReturnsViewWithNullModel()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.About() as ViewResult;

            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void Contact_Always_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Contact() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact_Always_ReturnsViewWithNullModel()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Contact() as ViewResult;

            Assert.IsNull(result.Model);
        }

        [TestMethod]
        public void Read_Always_ReturnsJson()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(3);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Read(new DataSourceRequest()
            {
                Page = 1,
                PageSize = 10
            }) as JsonResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Read_When5Movies_Returns5Json()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Read(new DataSourceRequest()
            {
                Page = 1,
                PageSize = 10
            }) as JsonResult;

            var kendoResult = result.Data as DataSourceResult;

            Assert.AreEqual(5, kendoResult.Total);
        }

        [TestMethod]
        public void Read_When100Movies_Returns100Json()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(100);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Read(new DataSourceRequest()
            {
                Page = 1,
                PageSize = 10
            }) as JsonResult;

            var kendoResult = result.Data as DataSourceResult;

            Assert.AreEqual(100, kendoResult.Total);
        }

        [TestMethod]
        public void Read_When0Movies_Returns0Json()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(0);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.Read(new DataSourceRequest()
            {
                Page = 1,
                PageSize = 10
            }) as JsonResult;

            var kendoResult = result.Data as DataSourceResult;

            Assert.AreEqual(0, kendoResult.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void MovieDetails_WhenNullParameter_ReturnsException()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.MovieDetails(null) as ViewResult;
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void MovieDetails_WhenNonExistantIdParameter_ReturnsException()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.MovieDetails(10) as ViewResult;
        }

        [TestMethod]
        public void MovieDetails_WhenValidId_ReturnsView()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.MovieDetails(3) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MovieDetails_WhenValidId_ReturnsMovieDetailsViewModel()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.MovieDetails(3) as ViewResult;

            var model = result.Model as MovieDetailsViewModel;

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void MovieDetails_WhenValidId_ReturnsMovieDetailsViewModelWithSameId()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.MovieDetails(3) as ViewResult;

            var model = result.Model as MovieDetailsViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual(3, model.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void SubtitleList_WhenNullMovieIdParameter_ReturnsException()
        {
            var fakeRepo = new Mock<IRepository<Movie>>();

            var fakeData = new Mock<IApplicationData>();

            var movies = ObjectGenerator.GetValidDifferentMovies(5);

            fakeRepo.Setup(f => f.All()).Returns(movies.AsQueryable());

            fakeData.Setup(f => f.Movies).Returns(fakeRepo.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleList(null, "1") as ViewResult;
        }

        [TestMethod]
        public void SubtitleList_WhenNonExistantMovieIdParameter_ReturnsPartialView()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleList(11, "1") as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubtitleList_WhenNonExistantMovieIdParameter_ReturnsPartialViewWith0Count()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleList(11, "1") as PartialViewResult;

            var model = result.Model as IEnumerable<SubtitleGridViewModel>;
            Assert.AreEqual(0, model.Count());
        }

        [TestMethod]
        public void SubtitleList_WhenCorrectMovieIdParameter_ReturnsPartialViewWithCorrectCount()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleList(1, "1") as PartialViewResult;

            var model = result.Model as IEnumerable<SubtitleGridViewModel>;
            Assert.AreEqual(5, model.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void SubtitleDetails_WhenNullIdParameter_ReturnsException()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleDetails(null) as ViewResult;
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void SubtitleDetails_WhenInvalidIdParameter_ReturnsException()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleDetails(111) as ViewResult;
        }

        [TestMethod]
        public void SubtitleDetails_WhenValidIdParameter_ReturnsView()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.SubtitleDetails(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void GetDetailsPartial_WhenNullIdParameter_ReturnsException()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.GetDetailsPartial(null) as ViewResult;
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void GetDetailsPartial_WhenInvalidIdParameter_ReturnsException()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.GetDetailsPartial(111) as ViewResult;
        }

        [TestMethod]
        public void GetDetailsPartial_WhenValidIdParameter_ReturnsPartialView()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeRepoMovies = new Mock<IRepository<Movie>>();
            var movies = ObjectGenerator.GetValidDifferentMovies(5);
            fakeRepoMovies.Setup(f => f.All()).Returns(movies.AsQueryable());
            fakeData.Setup(f => f.Movies).Returns(fakeRepoMovies.Object);

            var fakeRepoSubtitles = new Mock<IRepository<Subtitle>>();
            var subtitles = ObjectGenerator.GetValidDifferentSubtitles(5);
            fakeRepoSubtitles.Setup(f => f.All()).Returns(subtitles.AsQueryable());
            fakeData.Setup(f => f.Subtitles).Returns(fakeRepoSubtitles.Object);

            var controller = new HomeController(fakeData.Object);

            var result = controller.GetDetailsPartial(1) as PartialViewResult;

            Assert.IsNotNull(result);
        }

        
    }
}
