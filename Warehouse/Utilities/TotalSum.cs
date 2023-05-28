using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class TotalSum
    {
        public static string CalculateTotalSum(IEnumerable<Good> goods)
        {
            double totalSum = 0;

            foreach (Good good in goods)
            {
                totalSum += good.UnitPrice * good.Amount;
            }

            return FormatCurrency(totalSum);
        }
        private static string FormatCurrency(double amount)
        {
            return amount.ToString("#,##0.00", CultureInfo.InvariantCulture);
        }
    }
}
