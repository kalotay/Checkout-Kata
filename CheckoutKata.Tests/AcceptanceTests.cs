using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        public void GivenNoItemsPriceSHouldBeZero()
        {
            var checkout = 0;

            Assert.That(checkout, Is.EqualTo(0));
        }
    }
}