using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MyExpenses.Web.Common
{
    public static class PrincipalExtensions
    {
        public static MyExpensesUserIdentity GetAppIdentity(this IPrincipal principal)
        {
            var identity = principal.Identity as MyExpensesUserIdentity;
            return identity ?? MyExpensesPrincipal.Anonymous;
        }
    }
}