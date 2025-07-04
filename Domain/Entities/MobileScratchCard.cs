using FawryCSharpTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Domain.Entities
{
    public class MobileScratchCard : Product
    {
        public MobileScratchCard(string name, double price, int quantity)
            : base(name, price, quantity)
        {
        }
    }
}
