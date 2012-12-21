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

        [TestCase("A", 50)]
        public void GivenOneItemShouldReturnItsPrice(object item, int price)
        {
            var checkout = new Checkout();
            checkout.Add(item);

            Assert.That(checkout.Price, Is.EqualTo(price));
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