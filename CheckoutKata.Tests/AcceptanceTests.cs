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
            var discounts = new Dictionary<object, int>
                                {
                                    {'A', 20},
                                    {'B', 15}
                                };

            var discountQuantities = new Dictionary<object, int>
                                         {
                                             {'A', 3},
                                             {'B', 2}
                                         };

            _checkout = new Checkout(new PriceTotalizer(prices), discounts, discountQuantities);
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

    public class PriceTotalizer
    {
        private readonly IReadOnlyDictionary<object, int> _prices;

        public PriceTotalizer(IReadOnlyDictionary<object, int> prices)
        {
            _prices = prices;
        }

        public int PriceTotal { get; set; }

        public void ApplyPrice(object item)
        {
            PriceTotal += _prices[item];
        }
    }

    public class Checkout
    {
        private readonly IDictionary<object, int> _itemCount;
        private readonly IReadOnlyDictionary<object, int> _itemQuantityForDiscount;
        private readonly IReadOnlyDictionary<object, int> _discountAmount;
        private readonly PriceTotalizer _priceTotalizer;

        public Checkout(PriceTotalizer priceTotalizer, IReadOnlyDictionary<object, int> discountAmount, IReadOnlyDictionary<object, int> itemQuantityForDiscount)
        {
            _priceTotalizer = priceTotalizer;
            _itemCount = new Dictionary<object, int>();
            _itemQuantityForDiscount = itemQuantityForDiscount;
            _discountAmount = discountAmount;
        }

        public void Scan(object item)
        {
            IncrementItemCount(item);
            _priceTotalizer.ApplyPrice(item);
            ApplyDiscounts(item);
        }

        private void ApplyDiscounts(object item)
        {
            if (!_itemQuantityForDiscount.ContainsKey(item)) return;
            
            if (_itemCount[item] == _itemQuantityForDiscount[item])
            {
                DiscountTotal += _discountAmount[item];
            }
        }

        private void IncrementItemCount(object item)
        {
            if (_itemCount.ContainsKey(item))
            {
                _itemCount[item] += 1;
                return;
            }

            _itemCount.Add(item, 1);
        }

        public int Total { get { return _priceTotalizer.PriceTotal - DiscountTotal; } }

        protected int DiscountTotal  { get; set; }
    }
}