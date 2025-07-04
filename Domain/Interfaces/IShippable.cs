using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fawryTask.Domain.Interfaces
{
    public interface IShippable
    {
        string GetName();
        double GetWeight();
    }
}
