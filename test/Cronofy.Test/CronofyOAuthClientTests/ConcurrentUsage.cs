namespace Cronofy.Test.CronofyOAuthClientTests
{
    using System;
    using System.Threading.Tasks;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConcurrentUsage
    {
        [Test]
        public void CanUseCronofyOAuthClientConstructorConcurrently()
        {
            UrlProviderFactory.Reset();
            Parallel.For(0, 100, _ => new CronofyOAuthClient("id", "secret"));
        }
    }
}
