using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.IntegrationTests
{
    [TestClass]
    public class ExpensesTest : TestBase
    {
        public ExpensesTest() :base(true)
        {
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
                await Client.PostAsync("api/expenses/new", content);
            }

            var response = await Client.GetAsync("api/expenses");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var result = JArray.Parse(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(result.Count>=100);

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
            var response = await Client.PostAsync("api/expenses/new", content);
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
            var response = await Client.PostAsync("api/expenses/new", content);
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
            response = await Client.PostAsync("api/expenses/update", content);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            // make sure the update was performed correctly
            response = await Client.GetAsync($"api/expenses/{expenseId}");

            var serverVersionExpense = JObject.Parse(await response.Content.ReadAsStringAsync());
            serverVersionExpense.Remove("OwnerId"); // don't want to compare this property
            Assert.IsTrue(JToken.DeepEquals(JObject.FromObject(updatedExpense), serverVersionExpense));

        }

        [TestMethod]
        public async Task Can_Delete_Expense()
        {
            var expense = new
            {
                Amount = 100,
                Date = DateTime.Today,
                Description = "This is a Test"
            };
            var jsonExpense = JsonConvert.SerializeObject(expense);

            var content = new StringContent(jsonExpense, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync("api/expenses/new", content);
            var expenseId = int.Parse(await response.Content.ReadAsStringAsync());

            // Try to delete the expense
            response = await Client.DeleteAsync($"api/expenses/{expenseId}");
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);

            // Now, make sure the expense doesn't exists any more
            response = await Client.GetAsync($"api/expenses/{expenseId}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
