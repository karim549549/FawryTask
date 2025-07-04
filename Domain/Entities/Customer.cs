using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Domain.Entities
{
    public class Customer
    {
        public string Name { get; }
        private double _balance;
        private readonly object _lock = new();

        public Customer(string name, double balance)
        {
            Name = name;
            _balance = balance;
        }

        public double Balance
        {
            get
            {
                lock (_lock)
                {
                    return _balance;
                }
            }
        }

        public bool TryDeduct(double amount)
        {
            lock (_lock)
            {
                if (_balance < amount)
                    return false;

                _balance -= amount;
                return true;
            }
        }
    }
}
