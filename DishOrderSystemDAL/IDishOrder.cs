using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishOrderSystemDAL
{
    public interface IDishOrder
    {

        string Name { get; set; }
        string TypeId { get; set; }
        bool Multiple { get; set; }
        string GetQuantity(int dishCount);


    }
}
