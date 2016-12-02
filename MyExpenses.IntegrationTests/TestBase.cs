using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyExpenses.Web;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.IntegrationTests
{
    public class TestBase
    {
        private IDisposable _host;
        private HttpClient _client;
        private string _baseAddress = "http://localhost:5555";
        private bool _authenticate = false;

        public TestBase(bool authenticate)
        {
            _authenticate = authenticate;
        }

        public HttpClient Client
        {
            get { return _client; }
        }

        private async Task Authenticate()
        {
            var tokenRequestContent = new Dictionary<string, string>() { { "grant_type", "password" }, { "username", "admin" }, { "password", "admin" } };
            var content = new FormUrlEncodedContent(tokenRequestContent);
            var response = await _client.PostAsync("api/token", content);

            var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            string authenticationToken = (string)responseObject["access_token"];

            // generate the authentication token
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authenticationToken);
        }

        [TestInitialize]
        public void Init()
        {
            _host = WebApp.Start<Startup>(url: _baseAddress);
            _client = new HttpClient() { BaseAddress = new Uri(_baseAddress) };
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db"));
            if (!System.IO.Directory.Exists("db")) System.IO.Directory.CreateDirectory("db");

            if (_authenticate)
            {
                Task.Run(() => Authenticate()).Wait();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _host.Dispose();
        }

    }
}
