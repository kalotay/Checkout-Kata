using System.Collections.Generic;

namespace CheckoutKata
{
    public class DictDiscounter : IDiscounter
    {
        private readonly IDictionary<object, int> _itemCount;
        private readonly IReadOnlyDictionary<object, int> _itemQuantityForDiscount;
        private readonly IReadOnlyDictionary<object, int> _discountAmount;

        public DictDiscounter(IReadOnlyDictionary<object, int> itemQuantityForDiscount, IReadOnlyDictionary<object, int> discountAmount)
        {
            _itemCount = new Dictionary<object, int>();
            _itemQuantityForDiscount = itemQuantityForDiscount;
            _discountAmount = discountAmount;
        }

        public int Discount  { get; set; }

        public void Register(object item)
        {
            IncrementItemCount(item);
            ApplyDiscounts(item);
        }

        private void ApplyDiscounts(object item)
        {
            if (!_itemQuantityForDiscount.ContainsKey(item)) return;
            
            if (_itemCount[item] == _itemQuantityForDiscount[item])
            {
                Discount += _discountAmount[item];
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
    }
}