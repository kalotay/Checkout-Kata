using System.Collections;

namespace CheckoutKata
{
    public class Checkout
    {
        private readonly IPriceTotalizer _priceTotalizer;
        private readonly IDiscounter _discounter;

        public Checkout(IPriceTotalizer priceTotalizer, IDiscounter discounter)
        {
            _priceTotalizer = priceTotalizer;
            _discounter = discounter;
        }

        public void Scan(object item)
        {
            _priceTotalizer.Register(item);
            _discounter.Register(item);
        }

        public int Total { get { return _priceTotalizer.Total - _discounter.Discount; } }

        public void ScanAll(IEnumerable items)
        {
            foreach (var item in items)
            {
                Scan(item);
            }
        }
    }
}