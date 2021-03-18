namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    using NUnit.Framework;

    [TestFixture]
    internal abstract class Base
    {
        protected const string AccessToken = "zyxvut987654";

        protected CronofyEnterpriseConnectAccountClient Client { get; set; }

        protected StubHttpClient Http { get; set; }

        [SetUp]
        public void SetUp()
        {
            this.Client = new CronofyEnterpriseConnectAccountClient(AccessToken);
            this.Http = new StubHttpClient();

            this.Client.HttpClient = this.Http;
        }
    }
}
