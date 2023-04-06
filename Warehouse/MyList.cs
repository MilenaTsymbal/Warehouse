using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Warehouse
{
    internal class MyList : List<Goods>
    {
        public void AddExistingGoods()
        {
            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                Goods newProduct = new Goods(values[0], values[1], values[2], values[3], DateTime.Parse(values[4]));
                Add(newProduct);
            }
        }

        public void AddNewGoodsToFile()
        {
            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

            int lastLineNumber = File.ReadLines(filePath).Count();

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = lastLineNumber; i < Count; i++)
                {
                    /*string arrayString = allProducts[i].NameOfGood + "," + allProducts[i].UnitOfMeasure + "," + allProducts[i].UnitPrice + "," + allProducts[i].Amount + "," + allProducts[i].DateOfLastDelivery;*/
                    string arrayString = string.Join(",", this[i].NameOfGood, this[i].UnitOfMeasure, this[i].UnitPrice, this[i].Amount, this[i].DateOfLastDelivery);
                    writer.WriteLine(arrayString);
                }
            }
        }

        public void AddNewGoods()
        {
            string nameOfGood = "";
            string unitOfMeasure = "";
            string unitPrice = "";
            string amount = "";
            DateTime dateOfLastDelivery = DateTime.Parse("01.01.0001 00:00:00");

            int lastItem = Count;

            int numberOfNewProducts = Validator.GetTheValidationInt("Enter the number of goods you want to add: ");
            Console.WriteLine();

            for (int i = 1; i <= numberOfNewProducts; i++)
            {
                for (int j = 0; j< 5; j++)
                {
                    switch (j)
                    {
                        case 0:
                            nameOfGood = Validator.GetTheValidationString("Write name of a good: ");
                            break;
                        case 1:
                            unitOfMeasure = Validator.GetTheValidationString("Write unit of measure of a good: ");
                            break;
                        case 2:
                            unitPrice = Validator.GetTheValidationString("Write unit of price of a good: ");
                            break;
                        case 3:
                            amount = Validator.GetTheValidationString("Write amount of delivered goods: ");
                            break;
                        case 4:
                            dateOfLastDelivery = Validator.GetTheValidationTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;

                    }
                }
                Goods product = new Goods(nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);

                Add(product);
                Console.WriteLine();
            }
            Console.WriteLine();

            AddNewGoodsToFile();

            ProfitAndLossStatement(lastItem);
        }

        public void ProfitAndLossStatement(int lastItem)
        {
            int count = 1;

            Console.WriteLine("\t\t\t\tProfit and Loss Statement\n");
            Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21} |",
                "Number", "Name of a good", "Unit of measure", "Unit of price", "Amount", "Date of last delivery");

            for (int i = lastItem; i < Count; i++)
            {
                Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21:d} |",
                    count++,
                    this[i].NameOfGood,
                    this[i].UnitOfMeasure,
                    this[i].UnitPrice,
                    this[i].Amount,
                    this[i].DateOfLastDelivery);
            }
            Console.WriteLine();
            Console.WriteLine($"Total sum: {TotalSumOfNewGoods(lastItem)} uah");
        }

        public void ListOfAllGoods()
        {
            Console.WriteLine("\t\t\t\t\tList of all goods\n");
            Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21} |",
                "Number", "Name of a good", "Unit of measure", "Unit of price", "Amount", "Date of last delivery");

            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("| {0,-6} | {1,-14} | {2,-15} | {3,-14} | {4,-6} |{5,-21:d} |",
                    i + 1,
                    this[i].NameOfGood,
                    this[i].UnitOfMeasure,
                    this[i].UnitPrice,
                    this[i].Amount,
                    this[i].DateOfLastDelivery);
            }
            Console.WriteLine();
            Console.WriteLine($"Total sum: {TotalSumOfNewGoods()} uah");
        }

        public int TotalSumOfNewGoods(int lastItem)
        {
            int totalSum = 0;

            for(int i = lastItem; i < Count; i++)
            {
                string[] parts = this[i].UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(this[i].Amount);
            }

            return totalSum;
        }

        public int TotalSumOfNewGoods()
        {
            int totalSum = 0;

            foreach (Goods product in this)
            {
                string[] parts = product.UnitPrice.Split(' ');
                totalSum += int.Parse(parts[0]) * int.Parse(product.Amount);
            }

            return totalSum;
        }
    }
}
