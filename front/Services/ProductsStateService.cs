public class ProductsStateService
{
    public List<Product> Products { get; private set; }

    public event Action? OnChange;

    public void UpdateProducts(List<Product> newProducts)
    {
        Products = newProducts;
        NotifyStateChanged();
        Console.WriteLine("ProductsStateService Notfication sent.");
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}