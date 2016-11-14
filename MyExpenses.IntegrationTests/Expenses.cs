using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyExpenses.IntegrationTests
{
    [TestClass]
    public class Expenses : ApiTestBase
    {
        [TestMethod]
        public void Create_100_Expenses()
        {
            //var json = CreateJHero("Test", "Warrior", DateTime.Now, 10M, true);

            //var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

            //var result = await Client.PostAsync("api/heros", content);
            //Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            //result = await Client.GetAsync(result.Headers.Location);
            //Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            //var hero = JObject.Parse(await result.Content.ReadAsStringAsync());
            //hero.Remove("Id");
            //hero.Remove("Location");
            //Assert.IsTrue(JToken.DeepEquals(json, hero));

        }
    }
}
