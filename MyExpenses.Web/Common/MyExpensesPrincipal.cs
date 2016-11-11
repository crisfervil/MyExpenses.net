using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MyExpenses.Web.Common
{
    public class MyExpensesPrincipal : IPrincipal
    {
        public IIdentity Identity { get { return MyExpensesUserIdentity; } }
        public MyExpensesUserIdentity MyExpensesUserIdentity { get; private set; }
        public static MyExpensesUserIdentity Anonymous = new MyExpensesUserIdentity();

        private MyExpensesPrincipal()
        {
            MyExpensesUserIdentity = new MyExpensesUserIdentity();
        }

        public Boolean IsInRole(string role)
        {
            return false;
        }
    }
}