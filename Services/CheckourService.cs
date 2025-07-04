using fawryTask.Domain.Entities;
using fawryTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Services
{
    public class CheckoutService
    {
        private readonly ShippingService _shippingService;

        public CheckoutService()
        {
            _shippingService = new ShippingService();
        }

        public void Checkout(Customer customer, Cart cart)
        {
            if (cart.IsEmpty)
                throw new InvalidOperationException("Cart is empty.");

            double subtotal = 0;
            var shippableItems = new List<IShippable>();

            foreach (var item in cart.Items)
            {
                var product = item.Product;

                if (product.IsExpired)
                    throw new InvalidOperationException($"Product '{product.Name}' is expired.");

                
                if (!product.TryReserveQuantity(item.Quantity))
                    throw new InvalidOperationException($"Product '{product.Name}' is out of stock.");

                subtotal += item.TotalPrice;

                if (product is IShippable shippable)
                {
                    for (int i = 0; i < item.Quantity; i++)
                        shippableItems.Add(shippable);
                }
            }

            double shipping = CalculateShipping(shippableItems);
            double total = subtotal + shipping;

            if (!customer.TryDeduct(total))
                throw new InvalidOperationException("Insufficient balance.");

            _shippingService.ShipItems(shippableItems);
            PrintReceipt(cart.Items, subtotal, shipping, total, customer.Balance);
        }

        private double CalculateShipping(List<IShippable> items)
        {
            double totalWeight = items.Sum(item => item.GetWeight());
            return totalWeight > 0 ? 30.0 : 0.0;
        }

        private void PrintReceipt(IEnumerable<CartItem> items, double subtotal, double shipping, double total, double balance)
        {
            Console.WriteLine("\n** Checkout receipt **");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Quantity}x {item.Product.Name}\t{item.TotalPrice}");
            }
            Console.WriteLine("----------------------");
            Console.WriteLine($"Subtotal\t{subtotal}");
            Console.WriteLine($"Shipping\t{shipping}");
            Console.WriteLine($"Amount\t\t{total}");
            Console.WriteLine($"Remaining Balance: {balance}");
        }
    }
}
