using MyExpenses.Domain;
using MyExpenses.Web.Common;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MyExpenses.Web.Api
{
    public class ExpensesReadController : ApiController
    {
        private IExpensesDataContext _dataContext;

        public ExpensesReadController(IExpensesDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _dataContext.GetExpenses(User.GetAppIdentity().Id);
        }

        public Expense GetExpense(int id)
        {
            return _dataContext.GetExpense(id,User.GetAppIdentity().Id);
        }

    }
}
