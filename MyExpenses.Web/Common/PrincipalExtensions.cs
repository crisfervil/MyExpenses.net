using System.Security.Principal;

namespace MyExpenses.Web.Common
{
    public static class PrincipalExtensions
    {
        public static MyExpensesUserIdentity GetAppIdentity(this IPrincipal principal)
        {
            var identity = principal!=null? principal.Identity as MyExpensesUserIdentity:null;
            return identity ?? MyExpensesPrincipal.Anonymous;
        }
    }
}