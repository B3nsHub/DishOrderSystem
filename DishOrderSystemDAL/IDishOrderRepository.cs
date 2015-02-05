using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DishOrderSystemDAL;

namespace DishOrderSystemDAL //DishOrderSystem
{
    public interface IDishOrderRepository
    {
        IEnumerable<IDishOrder> GetDishOrders(string dayTime, IEnumerable<string> dishTypeIds);
    }
    
}
