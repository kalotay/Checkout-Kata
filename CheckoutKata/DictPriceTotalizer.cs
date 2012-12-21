using System.Collections.Generic;

namespace CheckoutKata
{
    public class DictPriceTotalizer : IPriceTotalizer
    {
        private readonly IReadOnlyDictionary<object, int> _prices;

        public DictPriceTotalizer(IReadOnlyDictionary<object, int> prices)
        {
            _prices = prices;
        }

        public int Total { get; private set; }

        public void Register(object item)
        {
            Total += _prices[item];
        }
    }
}