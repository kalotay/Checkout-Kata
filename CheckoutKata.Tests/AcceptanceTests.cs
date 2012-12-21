using System.Collections;
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
                                                         {'A', 50},
                                                         {'B', 30},
                                                         {'C', 20},
                                                         {'D', 15},
                                                     };
            var discounts = new Dictionary<object, DiscountSpec>
                                {
                                    {'A', new DiscountSpec {DiscountAmount = 20, ItemQuantity = 3}},
                                    {'B', new DiscountSpec {DiscountAmount = 15, ItemQuantity = 2}}
                                };

            _checkout = new Checkout(new DictPriceTotalizer(prices), new DictDiscounter(discounts));
        }

        [Test]
        public void GivenNoItemsPriceSHouldBeZero()
        {
            Assert.That(_checkout.Total, Is.EqualTo(0));
        }

        [TestCase('A', 50)]
        [TestCase('B', 30)]
        [TestCase('C', 20)]
        [TestCase('D', 15)]
        public void GivenOneItemShouldReturnItsPrice(object item, int price)
        {
            _checkout.Scan(item);

            Assert.That(_checkout.Total, Is.EqualTo(price));
        }

        [TestCase("AA", 100)]
        [TestCase("AC", 70)]
        [TestCase("AD", 65)]
        [TestCase("ADA", 115)]
        [TestCase("CCCCC", 100)]
        public void GivenNItemsWithoutDiscountReturnTheSumOfPrices(IEnumerable items, int price)
        {
            foreach (var item in items)
            {
                _checkout.Scan(item);
            }

            Assert.That(_checkout.Total, Is.EqualTo(price));
        }

        [Test]
        public void AppliesDiscountForA()
        {
            _checkout.Scan('A');
            _checkout.Scan('A');
            _checkout.Scan('A');

            Assert.That(_checkout.Total, Is.EqualTo(130));
        }

        [Test]
        public void AppliesDiscountForB()
        {
            _checkout.Scan('B');
            _checkout.Scan('B');

            Assert.That(_checkout.Total, Is.EqualTo(45));
        }

        [Test]
        public void AppliesDiscountForBothAAndB()
        {
            _checkout.Scan('A');
            _checkout.Scan('A');
            _checkout.Scan('B');
            _checkout.Scan('A');
            _checkout.Scan('B');

            Assert.That(_checkout.Total, Is.EqualTo(175));
        }

        [Test]
        public void AppliesDiscountTwice()
        {
            _checkout.Scan('B');
            _checkout.Scan('B');
            _checkout.Scan('B');
            _checkout.Scan('B');

            Assert.That(_checkout.Total, Is.EqualTo(90));
        }
    }
}