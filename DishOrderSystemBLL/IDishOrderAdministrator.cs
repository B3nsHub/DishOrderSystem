using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishOrderSystemBLL
{
    public interface IDishOrderAdministrator
    {
        string GetDishOrders(string dishes);
    }
}
