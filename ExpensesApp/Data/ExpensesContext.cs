using System.Data.Entity;

namespace ExpensesApp.Data
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
              new MigrateDatabaseToLatestVersion<ExpensesContext, ExpensesMigrationsConfiguration>()
              );
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}