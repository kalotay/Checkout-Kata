namespace CheckoutKata
{
    public interface IDiscounter
    {
        int Discount { get; set; }
        void Register(object item);
    }
}