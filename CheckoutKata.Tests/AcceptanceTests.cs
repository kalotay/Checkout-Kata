using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class AcceptanceTests
    {
        private Checkout _checkout;

        [SetUp]
        public void Setup()
        {
            _checkout = new Checkout();
        }

        [Test]
        public void GivenNoItemsPriceSHouldBeZero()
        {
            Assert.That(_checkout.Price, Is.EqualTo(0));
        }

        [TestCase("A", 50)]
        public void GivenOneItemShouldReturnItsPrice(object item, int price)
        {
            _checkout.Add(item);

            Assert.That(_checkout.Price, Is.EqualTo(price));
        }
    }

    public class Checkout
    {
        public void Add(object item)
        {
            if (item.Equals("A")) Price = 50;
        }

        public int Price { get; set; }
    }
}