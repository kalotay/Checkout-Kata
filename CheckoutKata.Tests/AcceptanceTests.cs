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
            _checkout = new Checkout(prices);
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

    }

    public class Checkout
    {
        private readonly IDictionary<object, int> _prices;
        private int _aCount;
        private int _bCount;

        public Checkout(IDictionary<object, int> prices)
        {
            _prices = prices;
            Total = 0;
            _aCount = 0;
            _bCount = 0;
        }

        public void Scan(object item)
        {
            IncrementItemCount(item);
            ApplyPrice(item);
            ApplyDiscounts();
        }

        private void ApplyPrice(object item)
        {
            Total += _prices[item];
        }

        private void ApplyDiscounts()
        {
            if (_aCount == 3)
            {
                Total -= 20;
                _aCount = 0;
            }
            if (_bCount == 2)
            {
                Total -= 15;
                _bCount = 0;
            }
        }

        private void IncrementItemCount(object item)
        {
            if (item.Equals('A')) _aCount += 1;
            if (item.Equals('B')) _bCount += 1;
        }

        public int Total { get; private set; }
    }
}