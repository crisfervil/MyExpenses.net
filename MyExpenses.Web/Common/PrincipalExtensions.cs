using System;
using System.Security.Claims;
using System.Security.Principal;

namespace MyExpenses.Web.Common
{
    public static class PrincipalExtensions
    {
        public static MyExpensesUserIdentity GetAppIdentity(this IPrincipal principal)
        {
            Guid? userId=null;
            string userName=string.Empty;
            bool isAuthenticated = false;
            var identity = principal!=null? principal.Identity as ClaimsIdentity : null;
            if (identity != null)
            {
                isAuthenticated = true;
                var nameClaim = identity.FindFirst(x => x.Type == "userName");
                userName = nameClaim != null ? nameClaim.Value : null;
                var idClaim = identity.FindFirst(x => x.Type == "userId");
                Guid parsedGuid;
                if (idClaim != null) if(Guid.TryParse(idClaim.Value, out parsedGuid)) userId=parsedGuid;
            }
            return new MyExpensesUserIdentity(userId,userName,isAuthenticated);
        }
    }
}