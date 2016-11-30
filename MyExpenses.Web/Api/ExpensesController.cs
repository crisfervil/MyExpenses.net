using MyExpenses.Domain;
using MyExpenses.Web.Common;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace MyExpenses.Web.Api
{
    public class ExpensesController : ApiController
    {
        private IExpensesDataContext _dataContext;

        public ExpensesController(IExpensesDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [Route("api/expenses")]
        public IEnumerable<Expense> GetExpenses()
        {
            return _dataContext.GetExpenses(User.GetAppIdentity().Id);
        }

        [Route("api/expenses/{id}")]
        public Expense GetExpense(int id)
        {
            var exp = _dataContext.GetExpense(id,User.GetAppIdentity().Id);
            if(exp==null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return exp;
        }

        [HttpPost]
        [Route("api/expenses/new")]
        public int Create(Expense expense)
        {
            return _dataContext.Create(expense, User.GetAppIdentity().Id);
        }

        [HttpPost]
        [Route("api/expenses/update")]
        public void Update(Expense expense)
        {
            _dataContext.Update(expense, User.GetAppIdentity().Id);
        }

        [HttpDelete]
        [Route("api/expenses/{id}")]
        public void Delete(int id)
        {
            _dataContext.Delete(id, User.GetAppIdentity().Id);
        }

    }
}
