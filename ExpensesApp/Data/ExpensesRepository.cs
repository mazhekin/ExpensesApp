using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace ExpensesApp.Data
{
    public interface IExpensesRepository
    {
        IQueryable<Expense> GetExpenses();
        IQueryable<Category> GetCategories();

        bool Add(Expense expense);

        bool Save();
    }

    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ExpensesContext _ctx;
        public ExpensesRepository(ExpensesContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Expense> GetExpenses()
        {
            return _ctx.Expenses.Include("Category");
        }

        public IQueryable<Category> GetCategories()
        {
            return _ctx.Categories;
        }

        public bool Add(Expense expense)
        {
            try
            {
                _ctx.Expenses.Add(expense);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: Log this error
                return false;
            }
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var ex in dbEx.EntityValidationErrors)
                {
                    var locEx = ex;
                }
                return false;
            }
            catch (Exception ex)
            {
                // TODO: Log this error
                return false;
            }
        }
    }
}