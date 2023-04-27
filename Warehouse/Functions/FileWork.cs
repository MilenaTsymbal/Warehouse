using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class FileWork
    {
        //AccessFile accessFileOfCars = AccessFile.GetAccessToFile("CarDB.txt", "..\..\..\MainFunctions\CarFunctions");
        private static string filePath = @"..\..\..\Goods\Goods.txt";
        public static void AddExistingGoods(Warehouse allGoods)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    switch (values[0])
                    {
                        case "food":
                        case "drinks":
                            allGoods.Add(new Food(values[0], values[1], values[2], int.Parse(values[3]), int.Parse(values[4]), values[5], values[6]));
                            break;
                        case "clothes":
                        case "footwear":
                            allGoods.Add(new Clothing(values[0], values[1], values[2], values[3], values[4], values[5], int.Parse(values[6]), int.Parse(values[7]), values[8]));
                            break;
                        case "electronics":
                            allGoods.Add(new Electronics(values[0], values[1], values[2], values[3], values[4], int.Parse(values[5]), int.Parse(values[6]), values[7]));
                            break;
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
        }

        public static void AddNewGoodsToFile(Warehouse allGoods)
        {
            int lastLineNumber = File.ReadLines(filePath).Count();
            string arrayString = "";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = lastLineNumber; i < allGoods.Count; i++)
                {
                    arrayString = FormatGood(allGoods[i]);
                    writer.WriteLine(arrayString);
                }
            }
        }

        public static void RewriteGoodsInFile(Warehouse allGoods)
        {

            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                string arrayString = "";

                foreach (Good good in allGoods)
                {
                    arrayString = FormatGood(good);
                    writer.WriteLine(arrayString);
                }
            }
        }

        private static string FormatGood(Good good)
        {
            if (good is Food food)
            {
                return string.Join(",", food.Category, food.NameOfGood, food.UnitOfMeasure, food.UnitPrice, food.Amount, food.ExpiryDate, food.DateOfLastDelivery);
            }
            else if (good is Clothing clothing)
            {
                return string.Join(",", clothing.Category, clothing.NameOfGood, clothing.Size, clothing.Color, clothing.Brand, clothing.UnitOfMeasure, clothing.UnitPrice, clothing.Amount, clothing.DateOfLastDelivery);
            }
            else if (good is Electronics electronics)
            {
                return string.Join(",", electronics.Category, electronics.NameOfGood, electronics.Model, electronics.Company, electronics.UnitOfMeasure, electronics.UnitPrice, electronics.Amount, electronics.DateOfLastDelivery);
            }
            else
            {
                throw new ArgumentException("Unsupported type of Good");
            }
        }

    }
}
