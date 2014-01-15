using System.Collections.Generic;
using System.Linq;
using ExpensesApp.Controllers;
using ExpensesApp.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExpensesApp.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private CategoriesController _controller;
        private Mock<IExpensesRepository> _expensesRepository;

        [TestInitialize]
        public void TestStartUp()
        {
            _expensesRepository = new Mock<IExpensesRepository>();
            _controller = new CategoriesController(_expensesRepository.Object);
        }

        [TestMethod]
        public void Get_Returns_Categories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "food" },
                new Category { Id = 2, Name = "bills" },
                new Category { Id = 3, Name = "taxi" }
            };
            _expensesRepository.Setup(x => x.GetCategories()).Returns(categories.AsQueryable());

            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsNotNull(result);
            var enumerable = result as Category[] ?? result.ToArray();
            Assert.AreEqual(enumerable.Count(), categories.Count);
            Assert.AreEqual(enumerable.ElementAt(0).Name, "bills");
            Assert.AreEqual(enumerable.ElementAt(1).Name, "food");
        }
    }
}
