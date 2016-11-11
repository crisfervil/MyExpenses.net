using MyExpenses.Domain;
using System.Data.Entity;

namespace MyExpenses.Data.EF
{
    class ExpensesDB: DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public ExpensesDB() : base("MyExpenses")
        {

        }

        public int GetNextSequenceValue(string sequenceName)
        {
            var rawQuery = Database.SqlQuery<long>($"SELECT NEXT VALUE FOR {sequenceName};");
            var task = rawQuery.SingleAsync();
            long nextVal = task.Result;
            return (int)nextVal;
        }
    }
}
