using Microsoft.AspNet.Identity;
using MyExpenses.Data.EF;
using MyExpenses.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace MyExpenses.Web.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private Domain.IExpensesDataContext _dataContext;

        public ExpensesController(Domain.IExpensesDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private Guid GetCurrentUserId()
        {
            var strCurrentUserId = User.Identity.GetUserId();
            var currentUserId = Guid.Parse(strCurrentUserId);
            return currentUserId;
        }

        private Expense Convert(Domain.Expense expense)
        {
            return new Expense() { Amount=expense.Amount, Date=expense.Date, Description=expense.Description, ExpenseId=expense.ExpenseId };
        }

        private Domain.Expense Convert(Expense expense)
        {
            return new Domain.Expense() { Amount = expense.Amount, Date = expense.Date, Description = expense.Description, ExpenseId = expense.ExpenseId };
        }

        // GET: Expenses
        public ActionResult Index()
        {
            List<Expense> expenses = null;
            var currentUserId = GetCurrentUserId();
            var result = _dataContext.GetExpenses(currentUserId);
            if (result != null) { expenses = result.ConvertAll(Convert); }
            return View(expenses);
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentUserId = GetCurrentUserId();
            var result = _dataContext.GetExpense(id.Value,currentUserId);
            if (result == null)
            {
                return HttpNotFound();
            }
            var expense = Convert(result);
            return View(expense);
        }

        // GET: Expenses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Amount,Date,Description")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                var createData = Convert(expense);
                createData.OwnerId = GetCurrentUserId();
                _dataContext.Create(createData);
                return RedirectToAction("Index");
            }
            // If there are validation errors, show the page again
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentUserId = GetCurrentUserId();
            var result = _dataContext.GetExpense(id.Value, currentUserId);
            if (result == null)
            {
                return HttpNotFound();
            }
            var expense = Convert(result);
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpenseId,Amount,Date,Description")] Expense expense)
        {
            if (ModelState.IsValid)
            {
                var editData = Convert(expense);
                editData.OwnerId = GetCurrentUserId();
                var saveOk = _dataContext.Update(editData);
                if (!saveOk)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Expenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentUserId = GetCurrentUserId();
            var result = _dataContext.GetExpense(id.Value, currentUserId);
            if (result == null)
            {
                return HttpNotFound();
            }
            var expense = Convert(result);
            return View(expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userId = GetCurrentUserId();
            var deleteOk = _dataContext.Delete(id, userId);
            if (!deleteOk)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
