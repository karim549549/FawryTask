using fawryTask.Domain.Entities;
using fawryTask.Services;

class Program
{
    static void Main(string[] args)
    {

        var cheese = new Cheese("Cheese", 100, 5, DateTime.Now.AddDays(2), 0.2); 
        var biscuits = new Biscuits("Biscuits", 150, 3, DateTime.Now.AddDays(2), 0.7); // 700g each
        var customer = new Customer("Karim", 1000);

        var cart = new Cart();
        cart.Add(cheese, 2);      
        cart.Add(biscuits, 1);   
        var checkout = new CheckoutService();
        try
        {
            checkout.Checkout(customer, cart);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Checkout failed: {ex.Message}");
        }
    }
}
