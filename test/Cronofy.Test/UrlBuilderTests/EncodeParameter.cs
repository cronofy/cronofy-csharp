namespace Cronofy.Test.UrlBuilderTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class EncodeParameter
    {
        [Test]
        public void EncodesSpaces()
        {
            Assert.AreEqual(
                "Hello%20World",
                UrlBuilder.EncodeParameter("Hello World"));
        }

        [Test]
        public void EncodesForwardSlashes()
        {
            Assert.AreEqual(
                "http%3A%2F%2Fhello.world",
                UrlBuilder.EncodeParameter("http://hello.world"));
        }
    }
}
