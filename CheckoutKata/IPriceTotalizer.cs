namespace CheckoutKata
{
    public interface IPriceTotalizer<in T>
    {
        int Total { get; }
        void Register(T item);
    }
}