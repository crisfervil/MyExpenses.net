using System;
using System.Collections.Generic;

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
