using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesApp.Data;

namespace ExpensesApp.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly IExpensesRepository _expensesRepository;
        public CategoriesController(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        public IEnumerable<Category> Get()
        {
            return _expensesRepository.GetCategories().OrderBy(x => x.Name);
        }

    }
}
