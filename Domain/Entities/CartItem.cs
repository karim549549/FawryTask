using FawryCSharpTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Domain.Entities
{
    public class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            Quantity = quantity;
        }

        public double TotalPrice => Product.Price * Quantity;
    }
}
