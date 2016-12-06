using Microsoft.AspNet.Identity.EntityFramework;

namespace MyExpenses.Web.Common
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthenticationDb")
        {

        }
    }
}