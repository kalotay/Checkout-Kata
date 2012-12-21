namespace CheckoutKata
{
    public interface IDiscounter<in T>
    {
        int Discount { get; set; }
        void Register(T item);
    }
}