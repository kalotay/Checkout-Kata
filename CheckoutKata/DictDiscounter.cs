using System.Collections.Generic;

namespace CheckoutKata
{
    public class DictDiscounter : IDiscounter
    {
        private readonly IDictionary<object, int> _itemCount;
        private readonly IReadOnlyDictionary<object, DiscountSpec> _discountSpecs; 

        public DictDiscounter(IReadOnlyDictionary<object, DiscountSpec> discountSpec)
        {
            _itemCount = new Dictionary<object, int>();
            _discountSpecs = discountSpec;
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
            
            if (_itemCount[item] == spec.ItemQuantity)
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