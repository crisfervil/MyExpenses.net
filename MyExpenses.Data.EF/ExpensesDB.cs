using MyExpenses.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Data.EF
{
    class ExpensesDB: DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public int GetNextSequenceValue(string sequenceName)
        {
            var rawQuery = Database.SqlQuery<long>($"SELECT NEXT VALUE FOR {sequenceName};");
            var task = rawQuery.SingleAsync();
            long nextVal = task.Result;
            return (int)nextVal;
        }
    }
}
