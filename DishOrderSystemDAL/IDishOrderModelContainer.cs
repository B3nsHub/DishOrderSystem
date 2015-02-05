using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DishOrderSystemDAL.Models;

namespace DishOrderSystemDAL
{
    public interface IDishOrderModelContainer
    {

            IDbSet<Dish_DishType> Dish_DishType { get; set; }
            IDbSet<Dish_TimeOfDay> Dish_TimeOfDay { get; set; }
            IDbSet<Dish> Dishes { get; set; }
            IDbSet<DishType> DishTypes { get; set; }
            IDbSet<TimeOfDay> TimeOfDays { get; set; }
        
    }
}
