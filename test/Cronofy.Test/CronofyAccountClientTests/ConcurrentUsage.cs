namespace Cronofy.Test.CronofyAccountClientTests
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConcurrentUsage
    {
        [Test]
        public void CanUseCronofyAccountClientConstructorConcurrently()
        {
            UrlProviderFactory.Reset();
            Parallel.For(0, 100, _ => new CronofyAccountClient("accesstoken"));
        }
    }
}
