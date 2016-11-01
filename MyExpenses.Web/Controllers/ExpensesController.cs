using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyExpenses.Web.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace MyExpenses.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private MyExpensesDbContext db = new MyExpensesDbContext();

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

            var expenses = from exp in db.Expenses
                           where exp.OwnerId == currentUserId
                           select exp;

            return View(expenses.ToList());
        }

        // GET: Expenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentUserId = GetCurrentUserId();
            Expense expense = db.Expenses.Find(id,currentUserId);
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
                expense.ExpenseId = db.GetNextSequenceValue("ExpensesIds");
                expense.OwnerId = GetCurrentUserId();

                db.Expenses.Add(expense);
                db.SaveChanges();
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
            Expense expense = db.Expenses.Find(new object[]{id,currentUserId});
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
                var currentUserId = GetCurrentUserId();
                var original = db.Expenses.Find(expense.ExpenseId,currentUserId);

                if (original == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                { 
                    original.Amount = expense.Amount;
                    original.Date = expense.Date;
                    original.Description = expense.Description;
                    db.SaveChanges();
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
            Expense expense = db.Expenses.Find(new object[] { id, currentUserId });
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
            var currentUserId = GetCurrentUserId();
            Expense expense = db.Expenses.Find(new object[] { id, currentUserId });
            db.Expenses.Remove(expense);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
