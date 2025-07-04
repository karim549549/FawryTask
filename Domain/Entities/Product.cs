namespace FawryCSharpTask.Models;

public abstract class Product
{
    public string Name { get; }
    public double Price { get; }

    private int _quantity;
    private readonly object _lock = new();

    protected Product(string name, double price, int quantity)
    {
        Name = name;
        if(price < 0) 
            throw  new ArgumentOutOfRangeException("Price cannot be negative.");
        Price = price;
        _quantity = quantity;
    }

    public int Quantity
    {
        get
        {
            lock (_lock) { return _quantity; }
        }
    }

    public virtual bool IsExpired => false;

    public bool IsAvailable(int requestedQty)
    {
        return Quantity >= requestedQty;
    }

    public bool TryReserveQuantity(int requestedQty)
    {
        lock (_lock)
        {
            if (!IsAvailable(requestedQty))
                return false;

            _quantity -= requestedQty;
            return true;
        }
    }
}
