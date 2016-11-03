using MyExpenses.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Data.EF
{
    public class ExpensesDataContext : IExpensesDataContext, IDisposable
    {
        ExpensesDB _db = new ExpensesDB();

        public void Create(Expense expense)
        {
            expense.ExpenseId = _db.GetNextSequenceValue("ExpensesIds");
            _db.Expenses.Add(expense);
            _db.SaveChanges();
        }

        public Expense GetExpense(int id, Guid userId)
        {
            return _db.Expenses.Find(id, userId);
        }

        public bool Delete(int id, Guid userId)
        {
            var expense = GetExpense(id, userId);
            if (expense == null)
            {
                return false;
            }
            else
            {
                _db.Expenses.Remove(expense);
                _db.SaveChanges();
            }
            // Everything went OK
            return true;
        }

        public List<Expense> GetExpenses(Guid userId)
        {
            var expenses = from exp in _db.Expenses
                           where exp.OwnerId == userId
                           select exp;
            return expenses.ToList();
        }

        public bool Update(Expense expense)
        {
            var original = GetExpense(expense.ExpenseId, expense.OwnerId);
            if (original == null)
            {
                return false;
            }
            else
            {
                original.Amount = expense.Amount;
                original.Date = expense.Date;
                original.Description = expense.Description;
                _db.SaveChanges();
            }
            // Everything went OK
            return true;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
