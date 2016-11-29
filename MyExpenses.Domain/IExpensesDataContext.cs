using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Domain
{
    public interface IExpensesDataContext : IDisposable
    {
        List<Expense> GetExpenses(Guid userId);
        Expense GetExpense(int id, Guid userId);
        bool Delete(int id, Guid userId);
        void Create(Expense expense, Guid userId);
        bool Update(Expense expense);
    }
}
