using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.IntegrationTests
{
    [TestClass]
    public class AccountTests : TestBase
    {
        public AccountTests() : base(false)
        {

        }

        [TestMethod]
        public async Task Can_Register_a_User_and_get_authentication_token()
        {
            var userName = $"TestUser{new Random().Next(int.MaxValue)}";
            var pwd = Guid.NewGuid().ToString();
            var newUser = new
            {
                UserName = userName,
                Password = pwd,
                ConfirmPassword = pwd
            };

            var jsonUser = JsonConvert.SerializeObject(newUser);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/Account/Register", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var token = base.GetToken(userName, pwd);
            Assert.IsNotNull(token);

        }

    }
}
