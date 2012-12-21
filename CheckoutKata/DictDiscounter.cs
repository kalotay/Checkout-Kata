using System.Collections.Generic;

namespace CheckoutKata
{
    public class DictDiscounter<T> : IDiscounter<T>
    {
        private readonly IDictionary<T, int> _itemCount;
        private readonly IReadOnlyDictionary<T, int> _itemQuantityForDiscount;
        private readonly IReadOnlyDictionary<T, int> _discountAmount;

        public DictDiscounter(IReadOnlyDictionary<T, int> itemQuantityForDiscount, IReadOnlyDictionary<T, int> discountAmount)
        {
            _itemCount = new Dictionary<T, int>();
            _itemQuantityForDiscount = itemQuantityForDiscount;
            _discountAmount = discountAmount;
        }

        public int Discount  { get; set; }

        public void Register(T item)
        {
            IncrementItemCount(item);
            ApplyDiscounts(item);
        }

        private void ApplyDiscounts(T item)
        {
            if (!_itemQuantityForDiscount.ContainsKey(item)) return;
            
            if (_itemCount[item] == _itemQuantityForDiscount[item])
            {
                Discount += _discountAmount[item];
            }
        }

        private void IncrementItemCount(T item)
        {
            if (_itemCount.ContainsKey(item))
            {
                _itemCount[item] += 1;
                return;
            }

            _itemCount.Add(item, 1);
        }
    }
}