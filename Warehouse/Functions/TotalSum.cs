using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class TotalSum
    {
        public static double CalculateTotalSum(IEnumerable<Good> goods)
        {
            double totalSum = 0;

            foreach (Good good in goods)
            {
                totalSum += good.UnitPrice * good.Amount;
            }

            return totalSum;
        }

        /*public static double NewGoods(Warehouse allGoods, int lastItem)
        {
            return CalculateTotalSum<Good>(allGoods.OfType<Good>().Skip(lastItem));
        }

        public static double AllGoods(Warehouse allGoods)
        {
            return CalculateTotalSum(allGoods);
        }

        public static double DeletedGoods(Warehouse deletedGoods)
        {
            return CalculateTotalSum(deletedGoods);
        }*/
    }
}
