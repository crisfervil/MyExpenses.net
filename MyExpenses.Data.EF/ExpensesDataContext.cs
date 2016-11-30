using MyExpenses.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyExpenses.Data.EF
{
    public class ExpensesDataContext : IExpensesDataContext, IDisposable
    {
        ExpensesDB _db = new ExpensesDB();

        /// <summary>
        /// Creates an Expense record and returns the Id of the generated expense
        /// </summary>
        public int Create(Expense expense,Guid userId)
        {
            var expenseId = _db.GetNextSequenceValue("ExpensesIds");
            expense.OwnerId = userId;
            expense.ExpenseId = expenseId;
            _db.Expenses.Add(expense);
            _db.SaveChanges();
            return expenseId;
        }

        public Expense GetExpense(int id, Guid userId)
        {
            return _db.Expenses.Find(id, userId);
        }

        public void Delete(int id, Guid userId)
        {
            var expense = GetExpense(id, userId);
            if (expense == null) throw new RecordNotFoundException();
            _db.Expenses.Remove(expense);
            _db.SaveChanges();
        }

        public List<Expense> GetExpenses(Guid userId)
        {
            var expenses = from exp in _db.Expenses
                           where exp.OwnerId == userId
                           select exp;
            return expenses.ToList();
        }

        public void Update(Expense expense, Guid userId)
        {
            var original = GetExpense(expense.ExpenseId, userId);
            if (original == null) throw new RecordNotFoundException();
            original.OwnerId = userId;
            original.Amount = expense.Amount;
            original.Date = expense.Date;
            original.Description = expense.Description;
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
