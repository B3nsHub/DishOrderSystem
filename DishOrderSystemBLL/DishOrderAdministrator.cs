using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DishOrderSystemDAL;
using DishOrderUtilities;

namespace DishOrderSystemBLL
{
    public class DishOrderAdministrator : IDishOrderAdministrator
    {

        private IDishOrderRepository _repository;

        public DishOrderAdministrator(IDishOrderRepository repository)
        {
            _repository = repository;
        }

        public DishOrderAdministrator()
        {          
            _repository = new DishOrderRepository(); 
        }

        public string GetDishOrders(string dishes)
        {
            bool isValidationError = true;
            var fullDishNameList = new List<string>();

            var itemList = dishes.Split(Char.Parse(Constants.DELIMITER)).Select(x => x.Trim()).ToList();

            if (itemList.Count > 1)
            {
                var timeOfDay = itemList.First();
                var dishTypeIds = itemList.Skip(1).ToList();
                var dishList = _repository.GetDishOrders(timeOfDay, dishTypeIds).ToList();

                var validDishTypeIdList = GetDishTypeIdListUpToPotentialError(dishTypeIds, dishList,
                    out isValidationError);


                fullDishNameList = validDishTypeIdList
                    .OrderBy(x => x.Key.TypeId)
                    .Select(dishDto => dishDto.Key.GetQuantity(dishDto.Value))
                    .ToList();
            }

            if (isValidationError) fullDishNameList.Add(Constants.ERROR);

            return string.Join(string.Format("{0} ", Constants.DELIMITER), fullDishNameList.ToArray());
        }

        private static IEnumerable<KeyValuePair<IDishOrder, int>> GetDishTypeIdListUpToPotentialError(
            IEnumerable<string> dishTypeIdList, IList<IDishOrder> dishList, out bool isValidationError)
        {
            isValidationError = false;
            var validDishTypeIdList = new Dictionary<IDishOrder, int>();

            foreach (var dishTypeId in dishTypeIdList)
            {
                var dishOrder = dishList.FirstOrDefault(dish => dish.TypeId == dishTypeId);
                if (dishOrder == null ||
                    (!dishOrder.Multiple && validDishTypeIdList.Count(x => x.Key.TypeId == dishOrder.TypeId) > 0))
                {
                    isValidationError = true;
                    break;
                }

                if (validDishTypeIdList.ContainsKey(dishOrder))
                    validDishTypeIdList[dishOrder]++;
                else
                    validDishTypeIdList.Add(dishOrder, 1);
            }

            return validDishTypeIdList;
        }
    }
}
