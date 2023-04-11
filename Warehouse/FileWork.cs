using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class FileWork
    {
        private static string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

        public void AddExistingGoods(List<Goods> allGoods)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                Goods newProduct = new Goods(values[0], values[1], values[2], values[3], DateTime.Parse(values[4]));
                allGoods.Add(newProduct);
            }
        }

        public static void AddNewGoodsToFile(List<Goods> allGoods)
        {
            int lastLineNumber = File.ReadLines(filePath).Count();

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = lastLineNumber; i < allGoods.Count; i++)
                {
                    /*string arrayString = allProducts[i].NameOfGood + "," + allProducts[i].UnitOfMeasure + "," + allProducts[i].UnitPrice + "," + allProducts[i].Amount + "," + allProducts[i].DateOfLastDelivery;*/
                    string arrayString = string.Join(",", allGoods[i].NameOfGood, allGoods[i].UnitOfMeasure, allGoods[i].UnitPrice, allGoods[i].Amount, allGoods[i].DateOfLastDelivery);
                    writer.WriteLine(arrayString);
                }
            }
        }

        public static void RewriteGoodsInFile(List<Goods> allGoods)
        {

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                for (int i = 0; i < allGoods.Count; i++)
                {
                    /*string arrayString = allProducts[i].NameOfGood + "," + allProducts[i].UnitOfMeasure + "," + allProducts[i].UnitPrice + "," + allProducts[i].Amount + "," + allProducts[i].DateOfLastDelivery;*/
                    string arrayString = string.Join(",", allGoods[i].NameOfGood, allGoods[i].UnitOfMeasure, allGoods[i].UnitPrice, allGoods[i].Amount, allGoods[i].DateOfLastDelivery);
                    writer.WriteLine(arrayString);
                }
            }
        }

    }
}
