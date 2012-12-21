using System.Collections.Generic;

namespace CheckoutKata
{
    public class DictDiscounter : IDiscounter
    {
        public struct DiscountSpec
        {
            public int ItemQuantity;
            public int DiscountAmount;
        }

        private readonly IDictionary<object, int> _itemCount;
        private readonly IReadOnlyDictionary<object, DiscountSpec> _discountSpecs; 

        public DictDiscounter(IReadOnlyDictionary<object, DiscountSpec> discountSpecs)
        {
            _itemCount = new Dictionary<object, int>();
            _discountSpecs = discountSpecs;
        }

        public int Discount  { get; set; }

        public void Register(object item)
        {
            IncrementItemCount(item);
            ApplyDiscounts(item);
        }

        private void ApplyDiscounts(object item)
        {
            if (!_discountSpecs.ContainsKey(item)) return;

            var spec = _discountSpecs[item];
            
            if ((_itemCount[item] % spec.ItemQuantity) == 0)
            {
                Discount += spec.DiscountAmount;
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