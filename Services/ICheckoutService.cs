using fawryTask.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Services
{
    internal interface ICheckoutService
    {
        void Checkout(Customer customer, Cart cart);
    }
}
