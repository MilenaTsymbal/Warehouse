using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class TotalSum
    {
        public static int NewGoods(List<Goods> allgoods, int lastItem)
        {
            int totalSum = 0;

            for (int i = lastItem; i < allgoods.Count; i++)
            {
                string[] parts = allgoods[i].UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(allgoods[i].Amount);
            }

            return totalSum;
        }

        public static int AllGoods(List<Goods> allgoods)
        {
            int totalSum = 0;

            foreach (Goods product in allgoods)
            {
                string[] parts = product.UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(product.Amount);
            }

            return totalSum;
        }

        public static int DeletedGoods(List<Goods> deletedGoods)
        {
            int totalSum = 0;

            foreach (Goods product in deletedGoods)
            {
                string[] parts = product.UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(product.Amount);
            }

            return totalSum;
        }

    }
}
