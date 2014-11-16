namespace SubtitleCommunitySystem.Test.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using SubtitleCommunitySystem.Data;
    using SubtitleCommunitySystem.Web;
    using SubtitleCommunitySystem.Web.Controllers;
    using SubtitleCommunitySystem.Web.Services;
    using SubtitleCommunitySystem.Web.ViewModels;

    [TestClass]
    public class SearchControllerTests
    {
        public SearchControllerTests()
        {
            MvcApplication.ConfigureAutoMapper();
        }

        [TestMethod]
        public void Index_WhenNullQuery_ReturnsView()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeCache = new Mock<ICacheService>();

            string searchQuery = null;

            var listOfObjects = ObjectGenerator.GetValidDifferentSubtitleSearchViewModels(5);

            fakeCache.Setup(c => c.GetTop200SearchResults(searchQuery)).Returns(listOfObjects.AsQueryable());

            var controller = new SearchController(fakeData.Object, fakeCache.Object);

            var result = controller.Index(searchQuery) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index_WhenNotNullQuery_ReturnsView()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeCache = new Mock<ICacheService>();

            string searchQuery = "things";

            var listOfObjects = ObjectGenerator.GetValidDifferentSubtitleSearchViewModels(5);

            fakeCache.Setup(c => c.GetTop200SearchResults(searchQuery)).Returns(listOfObjects.AsQueryable());

            var controller = new SearchController(fakeData.Object, fakeCache.Object);

            var result = controller.Index(searchQuery) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetResults_WhenAnyQuery_ReturnsPartialView()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeCache = new Mock<ICacheService>();

            string searchQuery = null;

            var listOfObjects = ObjectGenerator.GetValidDifferentSubtitleSearchViewModels(5);

            fakeCache.Setup(c => c.GetTop200SearchResults(searchQuery)).Returns(listOfObjects.AsQueryable());

            var controller = new SearchController(fakeData.Object, fakeCache.Object);

            var result = controller.GetResults(searchQuery) as PartialViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetResults_WhenAnyQuery_ReturnsViewWithCorrectModel()
        {
            var fakeData = new Mock<IApplicationData>();

            var fakeCache = new Mock<ICacheService>();

            string searchQuery = null;

            var listOfObjects = ObjectGenerator.GetValidDifferentSubtitleSearchViewModels(5);

            fakeCache.Setup(c => c.GetTop200SearchResults(searchQuery)).Returns(listOfObjects.AsQueryable());

            var controller = new SearchController(fakeData.Object, fakeCache.Object);

            var result = controller.GetResults(searchQuery) as PartialViewResult;

            Assert.IsNotNull(result);

            var actualCollection = result.Model as IEnumerable<SubtitleCacheSearchViewModel>;
            Assert.AreEqual(listOfObjects.Count(), actualCollection.Count());
            Assert.AreEqual(listOfObjects.First(), actualCollection.First());
            Assert.AreEqual(listOfObjects.Last(), actualCollection.Last());

            CollectionAssert.AreEquivalent(listOfObjects.ToList(), actualCollection.ToList());
        }
    }
}
