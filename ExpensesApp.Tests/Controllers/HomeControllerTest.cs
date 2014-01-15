using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpensesApp;
using ExpensesApp.Controllers;
using MvcContrib.TestHelper;

namespace ExpensesApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Get_Returns_Default_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.AreEqual(result.ShouldBe<ViewResult>(null).ViewName, "");
        }

        [TestMethod]
        public void About_Get_Returns_Default_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.About();

            // Assert
            Assert.AreEqual(result.ShouldBe<ViewResult>(null).ViewName, "");
        }

        [TestMethod]
        public void Contact_Get_Returns_Default_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Contact() as ViewResult;

            // Assert
            Assert.AreEqual(result.ShouldBe<ViewResult>(null).ViewName, "");
        }
    }
}
