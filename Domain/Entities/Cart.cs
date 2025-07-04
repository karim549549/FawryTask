using FawryCSharpTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Domain.Entities
{
    public class Cart
    {
        private readonly List<CartItem> _items = new();
        public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

        public void Add(Product product, int quantity)
        {
            if (!product.IsAvailable(quantity))
                throw new InvalidOperationException($"Not enough quantity for {product.Name}");

            _items.Add(new CartItem(product, quantity));
        }

        public bool IsEmpty => !_items.Any();
    }
}
