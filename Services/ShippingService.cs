using fawryTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Services
{
    public class ShippingService
    {
        public void ShipItems(List<IShippable> items)
        {
            if (items.Count == 0) return;

            Console.WriteLine("\n** Shipment notice **");
            var grouped = items.GroupBy(item => item.GetName());

            foreach (var group in grouped)
            {
                double totalWeight = group.Sum(item => item.GetWeight());
                Console.WriteLine($"{group.Count()}x {group.Key}\t{totalWeight * 1000}g");
            }

            double fullWeight = items.Sum(i => i.GetWeight());
            Console.WriteLine($"Total package weight {fullWeight}kg");
        }
    }
}
