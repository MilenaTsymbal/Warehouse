using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Warehouse
{
    internal class MyList : List<Goods>
    {
        public void AddExistingGoods(List<Goods> allGoods)
        {
            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                Goods newProduct = new Goods(values[0], values[1], values[2], values[3], DateTime.Parse(values[4]));
                allGoods.Add(newProduct);
            }
        }
        public void AddNewGoodsToFile(List<Goods> allGoods)
        {
            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

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

        public void AddNewGoods(List<Goods> allGoods)
        {
            string nameOfGood = "";
            string unitOfMeasure = "";
            string unitPrice = "";
            string amount = "";
            DateTime dateOfLastDelivery = DateTime.Parse("01.01.0001 00:00:00");

            int lastItem = allGoods.Count;

            int numberOfNewProducts = GetTheValidationInt("Enter the number of goods you want to add: ");
            Console.WriteLine();

            for (int i = 1; i <= numberOfNewProducts; i++)
            {
                for (int j = 0; j< 5; j++)
                {
                    switch (j)
                    {
                        case 0:
                            nameOfGood = GetTheValidationString("Write name of a good: ");
                            break;
                        case 1:
                            unitOfMeasure = GetTheValidationString("Write unit of measure of a good: ");
                            break;
                        case 2:
                            unitPrice = GetTheValidationString("Write unit of price of a good: ");
                            break;
                        case 3:
                            amount = GetTheValidationString("Write amount of delivered goods: ");
                            break;
                        case 4:
                            dateOfLastDelivery = GetTheValidationTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;

                    }
                }
                Goods product = new Goods(nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);

                allGoods.Add(product);
                Console.WriteLine();
            }
            Console.WriteLine();

            AddNewGoodsToFile(allGoods);

            ProfitAndLossStatement(allGoods, lastItem);
        }

        public void ProfitAndLossStatement(List<Goods> allGoods, int lastItem)
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
            Console.WriteLine($"Total sum: {TotalSumOfNewGoods(allGoods, lastItem)} uah");
        }

        public void ProfitAndLossStatement1(List<Goods> allGoods)//all the elements
        {
            Console.WriteLine("\t\t\t\tList of all goods\n");
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
            Console.WriteLine($"Total sum: {TotalSumOfNewGoods(allGoods)} uah");
        }

        public int TotalSumOfNewGoods(List<Goods> allGoods, int lastItem)
        {
            int totalSum = 0;

            for(int i = lastItem; i < allGoods.Count; i++)
            {
                string[] parts = allGoods[i].UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(allGoods[i].Amount);
            }

            return totalSum;
        }

        public int TotalSumOfNewGoods(List<Goods> allGoods)
        {
            int totalSum = 0;

            foreach (Goods product in allGoods)
            {
                string[] parts = product.UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(product.Amount);
            }

            return totalSum;
        }

        static string GetTheValidationString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        return input;
                    }
                    else
                    {
                        Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                    }
                }
                catch (FormatException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. Please enter an string value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                }
            }
        }

        static int GetTheValidationInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        return int.Parse(input);
                    }
                    else
                    {
                        Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                    }
                }
                catch (FormatException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. Please enter an string value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                }
            }
        }

        static DateTime GetTheValidationTime(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        return DateTime.Parse(input);
                    }
                    else
                    {
                        Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                    }
                }
                catch (FormatException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. Please enter an string value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                }
            }
        }

        static void Message(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
