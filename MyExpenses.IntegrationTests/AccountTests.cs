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
        public async Task CanRegisterAUSer()
        {
            var pwd = Guid.NewGuid().ToString();
            var newUser = new
            {
                UserName = $"TestUser{new Random().Next(int.MaxValue)}",
                Password = pwd,
                ConfirmPassword = pwd
            };

            var jsonUser = JsonConvert.SerializeObject(newUser);
            var content = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/Account/Register", content);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
