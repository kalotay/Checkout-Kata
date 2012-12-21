using System.Collections.Generic;

namespace CheckoutKata
{
    public class DictPriceTotalizer<T> : IPriceTotalizer<T>
    {
        private readonly IReadOnlyDictionary<T, int> _prices;

        public DictPriceTotalizer(IReadOnlyDictionary<T, int> prices)
        {
            _prices = prices;
        }

        public int Total { get; private set; }

        public void Register(T item)
        {
            Total += _prices[item];
        }
    }
}