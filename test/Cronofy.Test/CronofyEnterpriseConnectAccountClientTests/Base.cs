using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    [TestFixture]
    internal abstract class Base
    {
        protected const string AccessToken = "zyxvut987654";

        protected CronofyEnterpriseConnectAccountClient Client;
        protected StubHttpClient Http;

        [SetUp]
        public void SetUp()
        {
            this.Client = new CronofyEnterpriseConnectAccountClient(AccessToken);
            this.Http = new StubHttpClient();

            Client.HttpClient = Http;
        }
    }
}
