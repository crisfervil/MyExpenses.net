using Microsoft.Owin.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyExpenses.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.IntegrationTests
{
    [TestClass]
    public abstract class ApiTestBase
    {
        static string baseAddress = "http://localhost:9001/";

        IDisposable host;
        HttpClient _client;
        protected HttpClient Client
        {
            get { return _client; }
        }

        [TestInitialize]
        public void Init()
        {
            host = WebApp.Start<Startup>(url: baseAddress);
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }

        [TestCleanup]
        public void Cleanup()
        {
            host.Dispose();
        }
    }
}
