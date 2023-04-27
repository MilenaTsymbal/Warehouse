using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Warehouse
{
    internal class Print
    {
        public static void PrintGoods<T>(Warehouse goods, string title, List<Good>? totalSumOfGood = null) where T : Good
        {
            Console.WriteLine($"\n\t\t\t\t\t\t\t\t{title}\n");
            int counter = 1;
            var table = new ConsoleTable("№", "Category", "Name of a good", "Size", "Color", "Brand", "Model", "Company",
        "Unit of measure", "Unit of price", "Amount", "Expiry date", "Date of last delivery");

            foreach (Good item in goods)
            {
                table.AddRow(
                     counter++,
                        item.Category,
                        item.NameOfGood,
                        item is Clothing clothingSize ? clothingSize.Size : "",
                        item is Clothing clothingColor ? clothingColor.Color : "",
                        item is Clothing clothingBrand ? clothingBrand.Brand : "",
                        item is Electronics electronicsModel ? electronicsModel.Model : "",
                        item is Electronics electronicsCompany ? electronicsCompany.Company : "",
                        item.UnitOfMeasure,
                        $"{item.UnitPrice} uah/{item.UnitOfMeasure}",
                        item.Amount,
                        item is Food foodExpiryDate ? foodExpiryDate.ExpiryDate : "",
                        item.DateOfLastDelivery);

                if (totalSumOfGood != null)
                {
                    totalSumOfGood.Add(item);
                }
            }
            Console.Write(table.ToString());
            Console.WriteLine($"\n\n Total sum: {TotalSum.CalculateTotalSum(totalSumOfGood == null ? goods : totalSumOfGood!)} uah");
        }

        public static void PrintCategoryOfGoods<T>(string[] header, Warehouse goods, string title, List<Good> totalSumOfGood) where T : Good
        {
            Console.WriteLine($"\n\t\t\t\t\t\t{title}\n");
            var table = new ConsoleTable(header);

            int counter = 1;

            foreach (T good in goods.OfType<T>())
            {
                if (good is Food food)
                {
                    table.AddRow(
                    counter++,
                    food.NameOfGood,
                    food.UnitOfMeasure,
                    $"{food.UnitPrice} uah/{food.UnitOfMeasure}",
                    food.Amount,
                    food.ExpiryDate,
                    food.DateOfLastDelivery);
                    totalSumOfGood.Add(food);
                }
                else if (good is Clothing clothing)
                {
                    table.AddRow(
                    counter++,
                    clothing.NameOfGood,
                    clothing.Size,
                    clothing.Color,
                    clothing.Brand,
                    clothing.UnitOfMeasure,
                    $"{clothing.UnitPrice} uah/{clothing.UnitOfMeasure}",
                    clothing.Amount,
                    clothing.DateOfLastDelivery);
                    totalSumOfGood.Add(clothing);
                }
                else if (good is Electronics electronics)
                {
                    table.AddRow(
                    counter++,
                    electronics.NameOfGood,
                    electronics.Model,
                    electronics.Company,
                    electronics.UnitOfMeasure,
                    $"{electronics.UnitPrice} uah/{electronics.UnitOfMeasure}",
                    electronics.Amount,
                    electronics.DateOfLastDelivery);
                    totalSumOfGood.Add(electronics);
                }
            }
            Console.Write(table.ToString());
            Console.WriteLine($"\nTotal sum: {TotalSum.CalculateTotalSum(totalSumOfGood)} uah");
        }

        public static void PrintInvoice(Invoice invoice, string title)//all invoices
        {
            invoice.dateOfMakingInvoice = DateTime.Now;
            Console.WriteLine($"\n\t\t\t\t\t{title}\n");
            Console.WriteLine($"\n\t\t\t\t\t{invoice.dateOfMakingInvoice}\n");
            
            foreach (Warehouse warehouse in invoice)
            {
                PrintGoods<Good>(warehouse, $"Invoice {invoice.ToList().IndexOf(warehouse) + 1}");
            }
        }

        public static void WayOfPrinting(Warehouse allGoods)
        {
            Console.WriteLine("\n\nChoose the way of printing goods:\n\n1.Print list with all goods together\n\n2.Print goods divided into categories");
            Console.Write("\nEnter chosen option: ");

            string? input = Console.ReadLine();
            bool isValid = false;

            while (!isValid)
            {
                switch (input)
                {
                    case "1":
                        ListOfAllGoods(allGoods);
                        isValid = true;
                        break;
                    case "2":
                        PrintFood(allGoods);
                        PrintClothing(allGoods);
                        PrintElectronics(allGoods);
                        isValid = true;
                        break;
                    default:
                        Console.WriteLine("There is no option like this one. Try to rewrite it.");
                        break;
                }
            }
        }
        public static void PrintFood(Warehouse allGoods)
        {
            string[] headerForFood = {"№", "Name of a good", "Unit of measure", "Unit of price", "Amount",
                "Expiry date", "Date of last delivery"};
            PrintCategoryOfGoods<Food>(headerForFood, allGoods, "Food", new List<Good>());
        }
        public static void PrintClothing(Warehouse allGoods)
        {
            string[] headerForClothing = {"№", "Name of a good", "Size", "Color", "Brand", "Unit of measure", "Unit of price",
                "Amount", "Date of last delivery"};
            PrintCategoryOfGoods<Clothing>(headerForClothing, allGoods, "Clothing", new List<Good>());
        }
        public static void PrintElectronics(Warehouse allGoods)
        {
            string[] headerForElectronics = { "№", "Name of a good", "Model", "Company", "Unit of measure", "Unit of price",
                "Amount", "Date of last delivery" };
            PrintCategoryOfGoods<Electronics>(headerForElectronics, allGoods, "Electronics", new List<Good>());
        }

        public static void ListOfAllGoods(Warehouse allGoods)
        {
            PrintGoods<Good>(allGoods, "List of all goods");
        }
        public static void IncomeInvoice(Invoice incomeInvoice)
        {
            PrintInvoice(incomeInvoice, "Income invoice");
        }

        public static void ExpenceInvoice(Invoice expenceInvoice)
        {
            PrintInvoice(expenceInvoice, "Expence invoice");
        }
        public static void ListOfDeletedGoods(Invoice expenceInvoice)
        {
            PrintInvoice(expenceInvoice, "Expence invoice");
        }
        public static void ListAfetrEditing(Warehouse allGoods)
        {
            PrintGoods<Good>(allGoods, "Goods after editing");
        }
        /* public static void ListOfFindedGoods(Warehouse allGoods)
         {
             PrintGoods(allGoods, "Finded goods");
         }*/

        public static void Message(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}