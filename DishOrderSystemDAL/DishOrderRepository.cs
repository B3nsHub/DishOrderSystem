using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishOrderSystemDAL.Models;

namespace DishOrderSystemDAL
{
    public class DishOrderRepository : IDishOrderRepository
    {
        private IDishOrderModelContainer _entities;

        public DishOrderRepository()
        {
            _entities = new DishOrderEntities();
        }

        public DishOrderRepository(IDishOrderModelContainer dishOrderModelContainer)
        {
            _entities = dishOrderModelContainer;
        }

        public IEnumerable<IDishOrder> GetDishOrders(string timeOfDay, IEnumerable<string> dishTypeIds)
        {

            var dishList = from dish in _entities.Dishes
                           join dish_TimeOfDay in _entities.Dish_TimeOfDay on dish equals dish_TimeOfDay.Dish
                           join dish_DishType in _entities.Dish_DishType on dish equals dish_DishType.Dish
                           where
                               dish_TimeOfDay.TimeOfDay.Name.Trim() == timeOfDay && dishTypeIds.Contains(dish_DishType.DishType.Id.ToString())
                           select new DishOrder
                           {
                               Name = dish.Name.Trim(), TypeId = dish_DishType.DishType.Id.ToString(),
                               Multiple = (dish_TimeOfDay.EnableMultiple == true ? true : false)
                           };


            IEnumerable<IDishOrder> resultList = dishList.ToList();
            
            return resultList;
        }

    }



}
