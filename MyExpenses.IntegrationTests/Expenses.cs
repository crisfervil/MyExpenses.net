using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using MyExpenses.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MyExpenses.IntegrationTests
{
    [TestClass]
    public class Expenses
    {
        private IDisposable _host;
        private HttpClient _client;
        private string _baseAddress = "http://localhost:5555";

        [TestInitialize]
        public void Init()
        {
            _host = WebApp.Start<Startup>(url:_baseAddress);
            _client = new HttpClient() { BaseAddress = new Uri(_baseAddress) };
        }

        [TestCleanup]
        public void Cleanup()
        {
            _host.Dispose();
        }

        [TestMethod]
        public async Task Can_Get_All_Expenses()
        {
            var response = await _client.GetAsync("api/expenses");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task Can_Create_Expense()
        {
            var expense = new {
                Amount=100,
                Date=DateTime.Today,
                Description="This is a Test"
            };
            var jsonExpense = JsonConvert.SerializeObject(expense);

            var content = new StringContent(jsonExpense, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/expenses/new", content);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
