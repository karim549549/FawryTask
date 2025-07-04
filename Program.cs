using fawryTask.Domain.Entities;
using fawryTask.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Example 1: Successful checkout ===");

        var cheese1 = new Cheese("Cheese", 100, 5, DateTime.Now.AddDays(2), 0.2);
        var biscuits1 = new Biscuits("Biscuits", 150, 3, DateTime.Now.AddDays(2), 0.7);
        var customer1 = new Customer("Karim", 1000);
        var cart1 = new Cart();
        cart1.Add(cheese1, 2);
        cart1.Add(biscuits1, 1);

        var checkout1 = new CheckoutService();
        try
        {
            checkout1.Checkout(customer1, cart1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Checkout failed: {ex.Message}");
        }

        Console.WriteLine("\n=== Example 2: Quantity lock test ===");

        var cheese2 = new Cheese("Cheese", 100, 2, DateTime.Now.AddDays(2), 0.2);
        var customerA = new Customer("Ali", 1000);
        var customerB = new Customer("Sara", 1000);

        var cartA = new Cart();
        cartA.Add(cheese2, 2);

        var cartB = new Cart();
        cartB.Add(cheese2, 2); 

        var checkoutService2 = new CheckoutService();

        var taskA = Task.Run(() =>
        {
            try
            {
                checkoutService2.Checkout(customerA, cartA);
                Console.WriteLine("Customer A checked out successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Customer A failed: {ex.Message}");
            }
        });

        var taskB = Task.Run(() =>
        {
            try
            {
                checkoutService2.Checkout(customerB, cartB);
                Console.WriteLine("Customer B checked out successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Customer B failed: {ex.Message}");
            }
        });

        await Task.WhenAll(taskA, taskB);

        Console.WriteLine("\n=== Example 3: Balance lock test ===");

        var tv = new TV("TV", 600, 2, 3.0);
        var sharedCustomer = new Customer("Omar", 1000);

        var cartC = new Cart();
        cartC.Add(tv, 1);

        var cartD = new Cart();
        cartD.Add(tv, 1); 

        var checkoutService3 = new CheckoutService();

        var taskC = Task.Run(() =>
        {
            try
            {
                checkoutService3.Checkout(sharedCustomer, cartC);
                Console.WriteLine("Cart C checked out successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cart C failed: {ex.Message}");
            }
        });

        var taskD = Task.Run(() =>
        {
            try
            {
                checkoutService3.Checkout(sharedCustomer, cartD);
                Console.WriteLine("Cart D checked out successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cart D failed: {ex.Message}");
            }
        });

        await Task.WhenAll(taskC, taskD);
    }
}
