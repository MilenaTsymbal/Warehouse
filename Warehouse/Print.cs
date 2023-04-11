using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class Print
    {
        public static void ProfitAndLossStatement(List<Goods> allGoods, int lastItem)
        {
            int count = 1;

            Console.WriteLine("\t\t\t\tProfit and Loss Statement\n");
            Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21} |",
                "Number", "Name of a good", "Unit of measure", "Unit of price", "Amount", "Date of last delivery");

            for (int i = lastItem; i < allGoods.Count; i++)
            {
                Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21:d} |",
                    count++,
                    allGoods[i].NameOfGood,
                    allGoods[i].UnitOfMeasure,
                    allGoods[i].UnitPrice,
                    allGoods[i].Amount,
                    allGoods[i].DateOfLastDelivery);
            }
            Console.WriteLine();
            Console.WriteLine($"Total sum: {TotalSum.NewGoods(allGoods, lastItem)} uah");
        }

        public static void ListOfAllGoods(List<Goods> allGoods)
        {
            Console.WriteLine("\t\t\t\t\tList of all goods\n");
            Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21} |",
                "Number", "Name of a good", "Unit of measure", "Unit of price", "Amount", "Date of last delivery");

            for (int i = 0; i < allGoods.Count; i++)
            {
                Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21:d} |",
                    i + 1,
                    allGoods[i].NameOfGood,
                    allGoods[i].UnitOfMeasure,
                    allGoods[i].UnitPrice,
                    allGoods[i].Amount,
                    allGoods[i].DateOfLastDelivery);
            }
            Console.WriteLine();
            Console.WriteLine($"Total sum: {TotalSum.AllGoods(allGoods)} uah");
        }

        public static void ListOfDeletedGoods(List<Goods> deletedGoods)
        {
            Console.WriteLine("\t\t\t\t\tList of deleted goods\n");
            Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21} |",
                "Number", "Name of a good", "Unit of measure", "Unit of price", "Amount", "Date of last delivery");

            for (int i = 0; i < deletedGoods.Count; i++)
            {
                Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21:d} |",
                    i + 1,
                    deletedGoods[i].NameOfGood,
                    deletedGoods[i].UnitOfMeasure,
                    deletedGoods[i].UnitPrice,
                    deletedGoods[i].Amount,
                    deletedGoods[i].DateOfLastDelivery);
            }
            Console.WriteLine();
            Console.WriteLine($"Total sum: {TotalSum.DeletedGoods(deletedGoods)} uah");
        }
    }
}
