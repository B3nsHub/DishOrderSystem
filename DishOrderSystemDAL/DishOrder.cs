using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DishOrderSystemDAL
{
    public class DishOrder : IDishOrder
    {
        public string Name{ get; set; }
        public string TypeId{ get; set; }
        public bool Multiple{ get; set; }

        public string GetQuantity(int dishCount)
        {
            string retval = string.Empty;
            string quantity = string.Empty;

            if (dishCount > 1 && Multiple)
            {
                quantity = string.Format("(x{0})", dishCount);
            }

            retval = string.Format("{0}{1}", Name.Trim(), quantity);

            return retval;
        }


    }
}
