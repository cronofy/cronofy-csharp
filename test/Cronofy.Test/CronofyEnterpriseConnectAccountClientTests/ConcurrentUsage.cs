namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConcurrentUsage
    {
        [Test]
        public void CanUseCronofyEnterpriseConnectAccountClientConstructorConcurrently()
        {
            UrlProviderFactory.Reset();
            Parallel.For(0, 100, _ => new CronofyEnterpriseConnectAccountClient("accesstoken"));
        }
    }
}
