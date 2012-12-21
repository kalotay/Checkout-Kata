using System.Collections.Generic;
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
            var prices = new Dictionary<object, int>
                                                     {
                                                         {"A", 50},
                                                         {"B", 30},
                                                         {"C", 20},
                                                         {"D", 15},
                                                     };
            _checkout = new Checkout(prices);
        }

        [Test]
        public void GivenNoItemsPriceSHouldBeZero()
        {
            Assert.That(_checkout.Price, Is.EqualTo(0));
        }

        [TestCase("A", 50)]
        [TestCase("B", 30)]
        [TestCase("C", 20)]
        [TestCase("D", 15)]
        public void GivenOneItemShouldReturnItsPrice(object item, int price)
        {
            _checkout.Add(item);

            Assert.That(_checkout.Price, Is.EqualTo(price));
        }
    }

    public class Checkout
    {
        private readonly IDictionary<object, int> _prices;

        public Checkout(IDictionary<object, int> prices)
        {
            _prices = prices;
        }

        public void Add(object item)
        {
            Price = _prices[item];
        }

        public int Price { get; set; }
    }
}