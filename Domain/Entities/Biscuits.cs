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
    public class Biscuits : Product, IShippable
    {
        public DateTime ExpiryDate { get; }
        public double Weight { get; }

        public Biscuits(string name, double price, int quantity, DateTime expiryDate, double weight)
            : base(name, price, quantity)
        {
            ExpiryDate = expiryDate;
            Weight = weight;
        }

        public override bool IsExpired => DateTime.Now > ExpiryDate;

        public string GetName() => Name;
        public double GetWeight() => Weight;
    }

}
