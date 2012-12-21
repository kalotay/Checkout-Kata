namespace CheckoutKata
{
    public interface IPriceTotalizer
    {
        int Total { get; }
        void Register(object item);
    }
}