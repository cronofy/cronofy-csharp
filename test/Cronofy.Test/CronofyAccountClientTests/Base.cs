using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    internal abstract class Base
    {
        protected const string AccessToken = "zyxvut987654";

        protected CronofyAccountClient Client;
        protected StubHttpClient Http;

        [SetUp]
        public void SetUp()
        {
            this.Client = new CronofyAccountClient(AccessToken);
            this.Http = new StubHttpClient();

            Client.HttpClient = Http;
        }
    }
}
