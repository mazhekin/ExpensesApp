using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesApp.Data;

namespace ExpensesApp.Controllers
{
    public class ExpensesController : ApiController
    {
        private readonly IExpensesRepository _expensesRepository;
        public ExpensesController(IExpensesRepository expensesRepository)
        {
            _expensesRepository = expensesRepository;
        }

        public IEnumerable<Expense> Get()
        {
            return _expensesRepository.GetExpenses()
                .OrderByDescending(x => x.Created)
                .Take(20);
        }

        public IEnumerable<Expense> Get(int categoryId)
        {
            return _expensesRepository.GetExpenses()
                .Where(x => x.CategoryId == categoryId)
                .OrderByDescending(x => x.Created)
                .Take(20);
        }

        public HttpResponseMessage Post([FromBody]Expense expense)
        {
            if (expense.Created == default(DateTime))
            {
                expense.Created = DateTime.UtcNow;
            }

            if (_expensesRepository.Add(expense) && _expensesRepository.Save())
            {
                expense.Category = _expensesRepository.GetCategories()
                    .First(x => x.Id == expense.CategoryId);

                return Request.CreateResponse(HttpStatusCode.Created, expense);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
