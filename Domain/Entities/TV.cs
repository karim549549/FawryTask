using FawryCSharpTask.Models;
using fawryTask.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace fawryTask.Domain.Entities
{
    public class TV : Product, IShippable
    {
        public double Weight { get; }

        public TV(string name, double price, int quantity, double weight)
            : base(name, price, quantity)
        {
            Weight = weight;
        }

        public string GetName() => Name;
        public double GetWeight() => Weight;
    }

}
