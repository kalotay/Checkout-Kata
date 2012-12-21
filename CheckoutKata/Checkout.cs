namespace CheckoutKata
{
    public class Checkout<T>
    {
        private readonly IPriceTotalizer<T> _priceTotalizer;
        private readonly IDiscounter<T> _discounter;

        public Checkout(IPriceTotalizer<T> priceTotalizer, IDiscounter<T> discounter)
        {
            _priceTotalizer = priceTotalizer;
            _discounter = discounter;
        }

        public void Scan(T item)
        {
            _priceTotalizer.Register(item);
            _discounter.Register(item);
        }

        public int Total { get { return _priceTotalizer.Total - _discounter.Discount; } }
    }
}