using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MyExpenses.Web.Common
{
    public class MyExpensesUserIdentity : IIdentity
    {
        public Guid Id { get; private set; }
        public String Name { get; private set; }
        public String AuthenticationType { get; private set; }
        public Boolean IsAuthenticated { get; private set; }


        public MyExpensesUserIdentity()
        {
            Id = Guid.Empty;
            Name = "Anonymous";
            IsAuthenticated = false;
        }
    }
}