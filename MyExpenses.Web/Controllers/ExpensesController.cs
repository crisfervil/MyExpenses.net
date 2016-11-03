using Microsoft.AspNet.Identity;
using MyExpenses.Data.EF;
using MyExpenses.Domain;
using System;
using System.Net;
using System.Web.Mvc;

namespace MyExpenses.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private IExpensesDataContext _dataContext = new ExpensesDataContext();

        private Guid GetCurrentUserId()
        {
            var strCurrentUserId = User.Identity.GetUserId();
            var currentUserId = Guid.Parse(strCurrentUserId);
            return currentUserId;
        }

        // GET: Expenses
        public ActionResult Index()
        {
            var currentUserId = GetCurrentUserId();
            var expenses = _dataContext.GetExpenses(currentUserId);
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
            Expense expense = _dataContext.GetExpense(id.Value,currentUserId);
            if (expense == null)
            {
                return HttpNotFound();
            }
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
                expense.OwnerId = GetCurrentUserId();
                _dataContext.Create(expense);
                return RedirectToAction("Index");
            }

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
            Expense expense = _dataContext.GetExpense(id.Value, currentUserId);
            if (expense == null)
            {
                return HttpNotFound();
            }
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
                expense.OwnerId = GetCurrentUserId();
                var saveOk = _dataContext.Update(expense);
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
            Expense expense = _dataContext.GetExpense(id.Value, currentUserId);
            if (expense == null)
            {
                return HttpNotFound();
            }
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
