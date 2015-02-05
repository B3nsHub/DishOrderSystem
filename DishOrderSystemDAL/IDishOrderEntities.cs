using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DishOrderSystemDAL.Models;

namespace DishOrderSystemDAL
{
    public interface IDishOrderEntities
    {
        DbSet<Dish_DishType> Dish_DishType { get; set; }
        DbSet<Dish_TimeOfDay> Dish_TimeOfDay { get; set; }
        DbSet<Dish> Dishes { get; set; }
        DbSet<DishType> DishTypes { get; set; }
        DbSet<TimeOfDay> TimeOfDays { get; set; }
    }
}
