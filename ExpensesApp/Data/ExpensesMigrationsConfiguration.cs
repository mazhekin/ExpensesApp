using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ExpensesApp.Data
{
    public class ExpensesMigrationsConfiguration : DbMigrationsConfiguration<ExpensesContext>
    {
        public ExpensesMigrationsConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExpensesContext context)
        {
            base.Seed(context);
#if DEBUG
            if (!context.Categories.Any() && !context.Expenses.Any())
            {
                var categories = new List<Category>
                {
                    new Category() { Name = "food" },
                    new Category() { Name = "bills" },
                    new Category() { Name = "taxi" },
                    new Category() { Name = "entertainment" },
                };

                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }

                var now = DateTime.Now;
                var expenses = new List<Expense>
                {
                    new Expense() { Amount = 12, Comment = "comment1", Created = now - TimeSpan.FromDays(1), Category = categories[0] },
                    new Expense() { Amount = 45, Comment = "comment2", Created = now - TimeSpan.FromDays(2), Category = categories[1] },
                    new Expense() { Amount = 1.67M, Comment = "comment3", Created = now - TimeSpan.FromDays(3), Category = categories[2] },
                    new Expense() { Amount = 56.6M, Comment = "comment4", Created = now - TimeSpan.FromDays(4), Category = categories[3] },
                };

                foreach (var expense in expenses)
                {
                    context.Expenses.Add(expense);
                }

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif
        }
    }
}
