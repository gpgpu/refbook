using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;

using Moq;
using WebUI;

namespace RefbookTest
{
    [TestClass]
    public class Routing
    {
        [TestMethod]
        public void Parent_ParentId_Route()
        {
            // arrange
            var routes = new RouteCollection();
            WebUI.RouteConfig.RegisterRoutes(routes);

            RouteData routeData = null;
            // action
            var expectedRoute = "{parent}/{parentId}/{controller}/{action}/{id}";
            routeData = GetRouteDataForUrl("~/Portal/1/Book/List", routes);
            // assert
            Assert.AreEqual(((Route)(routeData.Route)).Url, expectedRoute);
            Assert.AreEqual(routeData.Values["parent"], "Portal");
            
            
            
        }

        private static RouteData GetRouteDataForUrl(string url, RouteCollection routes)
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath).Returns(url);

            var routeData = routes.GetRouteData(httpContextMock.Object);
            Assert.IsNotNull(routeData, "Should have a route");
            return routeData;

        }

        [TestMethod]
        public void StringFormat()
        {
            double d = 12.345;
            string result = d.ToString(".0000");
            string expected = "12.3500";

            Assert.AreEqual(expected, result);
        }
    }

    
}
