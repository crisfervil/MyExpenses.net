using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Owin.Hosting;
using MyExpenses.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MyExpenses.IntegrationTests
{
    [TestClass]
    public class ExpensesTest
    {
        private IDisposable _host;
        private HttpClient _client;
        private string _baseAddress = "http://localhost:5555";

        [TestInitialize]
        public void Init()
        {
            _host = WebApp.Start<Startup>(url:_baseAddress);
            _client = new HttpClient() { BaseAddress = new Uri(_baseAddress) };
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db"));
            if (!System.IO.Directory.Exists("db")) System.IO.Directory.CreateDirectory("db");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _host.Dispose();
        }

        [TestMethod]
        public async Task Can_Create_100_records_and_Get_All_Expenses()
        {
            // create 100 expenses
            for (int i = 0; i < 100; i++)
            {
                var expense = new
                {
                    Amount = 100,
                    Date = DateTime.Today,
                    Description = $"Expense #{i}"
                };
                var jsonExpense = JsonConvert.SerializeObject(expense);

                var content = new StringContent(jsonExpense, Encoding.UTF8, "application/json");
                await _client.PostAsync("api/expenses/new", content);
            }

            var response = await _client.GetAsync("api/expenses");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = JArray.Parse(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(result.Count>100);

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
            // make sure returns the Id
            var expenseId = int.Parse(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(expenseId>0);
        }

        [TestMethod]
        public async Task Can_Update_Expense()
        {
            var expense = new
            {
                Amount = 100,
                Date = DateTime.Today,
                Description = "This is a Test"
            };
            var jsonExpense = JsonConvert.SerializeObject(expense);

            var content = new StringContent(jsonExpense, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/expenses/new", content);
            var expenseId = int.Parse(await response.Content.ReadAsStringAsync());


            var updatedExpense = new
            {
                ExpenseId=expenseId,
                Amount = 255.34,
                Date = DateTime.Today.AddDays(1),
                Description = "This is a Test Updated"
            };

            jsonExpense = JsonConvert.SerializeObject(updatedExpense);
            content = new StringContent(jsonExpense, Encoding.UTF8, "application/json");
            response = await _client.PostAsync("api/expenses/update", content);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            // make sure the update was performed correctly
            response = await _client.GetAsync($"api/expenses/{expenseId}");

            var serverVersionExpense = JObject.Parse(await response.Content.ReadAsStringAsync());
            serverVersionExpense.Remove("OwnerId"); // don't want to compare this property
            Assert.IsTrue(JToken.DeepEquals(JObject.FromObject(updatedExpense), serverVersionExpense));

        }
    }
}
