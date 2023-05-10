using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace Warehouse
{
    internal class Print
    {
        public delegate void GoodsPrinter(Warehouse allGoods);
        public static void WayOfPrinting(Warehouse allGoods, GoodsPrinter? printGoodsTogether = null, bool printCategoriesOfGoods = false)
        {
            Console.WriteLine("\n\nChoose the way of printing goods:\n\n1.Print all goods together\n\n2.Print goods divided into categories");
            Console.Write("\nEnter the chosen option: ");

            string? input = Console.ReadLine();
            bool isValid = false;

            while (!isValid)
            {
                switch (input)
                {
                    case "1":
                        if(printGoodsTogether != null)
                        {
                            printGoodsTogether(allGoods);
                        }
                        isValid = true;
                        break;
                    case "2":
                        if(printCategoriesOfGoods == false)
                        {
                            PrintGoodCategories(allGoods);
                        }
                        else
                        {
                            PrintGoodCategories(allGoods, true);
                        }
                        isValid = true;
                        break;
                    default:
                        Message(ConsoleColor.Red, "\nInvalid command! Try to rewrite it.\n");
                        WayOfPrinting(allGoods);
                        break;
                }
            }
        }


        public static void PrintGoods<T>(Warehouse goods, string title, DateTime? dateOfMakingInvoice = null, List<Good>? totalSumOfGood = null) where T : Good
        {
            Console.WriteLine($"\n\t\t\t\t\t\t\t\t{title}\n");
            if(dateOfMakingInvoice != null)
            {
                Console.WriteLine($"\n\t\t\t\t\t\t\t\t{dateOfMakingInvoice}\n");
            }
            int counter = 1;
            var table = new ConsoleTable("№", "Category", "Name of a good", "Size", "Color", "Brand", "Model", "Company",
        "Unit of measure", "Unit of price", "Amount", "Expiry date", "Date of last delivery");

            foreach (Good item in goods)
            {
                table.AddRow(
                     counter++,
                        item.Category.ToLower(),
                        item.NameOfGood.ToLower(),
                        item is Clothing clothingSize ? clothingSize.Size.ToLower() : "",
                        item is Clothing clothingColor ? clothingColor.Color.ToLower() : "",
                        item is Clothing clothingBrand ? (clothingBrand.Brand.Substring(0, 1).ToUpper() + clothingBrand.Brand.Substring(1).ToLower()) : "",
                        item is Electronics electronicsModel ? electronicsModel.Model : "",
                        item is Electronics electronicsCompany ? (electronicsCompany.Company.Substring(0, 1).ToUpper() + electronicsCompany.Company.Substring(1).ToLower()) : "",
                        item.UnitOfMeasure,
                        $"{item.UnitPrice} uah/{item.UnitOfMeasure}",
                        item.Amount,
                        item is Food foodExpiryDate ? foodExpiryDate.ExpiryDate.ToShortDateString() : "",
                        item.DateOfLastDelivery);

                if (totalSumOfGood != null)
                {
                    totalSumOfGood.Add(item);
                }
            }

            Console.Write(table.ToString());
            Console.WriteLine($"\n\n Total sum: {TotalSum.CalculateTotalSum(totalSumOfGood == null ? goods : totalSumOfGood!)} uah\n");
        }


        private static void PrintCategoryOfGoods<T>(string[] header, Warehouse goods, string title, List<Good> totalSumOfGood, bool? printEmptyLists = null) where T : Good
        {
            var table = new ConsoleTable(header);

            int counter = 1;

            foreach (T good in goods.OfType<T>())
            {
                if (good is Food food)
                {
                    table.AddRow(
                    counter++,
                    food.NameOfGood.ToLower(),
                    food.UnitOfMeasure.ToLower(),
                    $"{food.UnitPrice} uah/{food.UnitOfMeasure}",
                    food.Amount,
                    food.ExpiryDate.ToShortDateString(),
                    food.DateOfLastDelivery);
                    totalSumOfGood.Add(food);
                }
                else if (good is Clothing clothing)
                {
                    table.AddRow(
                    counter++,
                    clothing.NameOfGood.ToLower(),
                    clothing.Size.ToLower(),
                    clothing.Color.ToLower(),
                    clothing.Brand,
                    clothing.UnitOfMeasure.ToLower(),
                    $"{clothing.UnitPrice} uah/{clothing.UnitOfMeasure}",
                    clothing.Amount,
                    clothing.DateOfLastDelivery);
                    totalSumOfGood.Add(clothing);
                }
                else if (good is Electronics electronics)
                {
                    table.AddRow(
                    counter++,
                    electronics.NameOfGood.ToLower(),
                    electronics.Model,
                    electronics.Company,
                    electronics.UnitOfMeasure.ToLower(),
                    $"{electronics.UnitPrice} uah/{electronics.UnitOfMeasure}",
                    electronics.Amount,
                    electronics.DateOfLastDelivery);
                    totalSumOfGood.Add(electronics);
                }
            }
            if (table.Rows.Count > 0)
            {
                Console.WriteLine($"\n\t\t\t\t\t\t{title}\n");
                Console.Write(table.ToString());
                Console.WriteLine($"\n\n Total sum: {TotalSum.CalculateTotalSum(totalSumOfGood == null ? goods : totalSumOfGood!)} uah");
            }
            else if (printEmptyLists == true)
            {
                PrintEmptyListMessage<T>();
            }
        }
        private static void PrintEmptyListMessage<T>()
        {
            if (typeof(T) == typeof(Food))
            {
                Message(ConsoleColor.Red, $"\n No food goods were found.");
            }
            else if (typeof(T) == typeof(Clothing))
            {
                Message(ConsoleColor.Red, $"\n No clothing goods were found.");
            }
            else if (typeof(T) == typeof(Electronics))
            {
                Message(ConsoleColor.Red, $"\n No electronic goods were found.\n");
            }
        }
        private static void PrintGoodCategories(Warehouse allGoods, bool printCategoriesOfGoods = false)
        {
            PrintCategoryOfGoods<Food>(new string[]{"№", "Name of a good", "Unit of measure", "Unit of price", "Amount",
                "Expiry date", "Date of last delivery"}, allGoods, "Food", new List<Good>(), printCategoriesOfGoods);

            PrintCategoryOfGoods<Clothing>(new string[] {
                "№", "Name of a good", "Size", "Color", "Brand", "Unit of measure", "Unit of price",
                "Amount", "Date of last delivery"}, allGoods, "Clothing", new List<Good>(), printCategoriesOfGoods);

            PrintCategoryOfGoods<Electronics>(new string[]{ "№", "Name of a good", "Model", "Company", "Unit of measure", "Unit of price",
                "Amount", "Date of last delivery" }, allGoods, "Electronics", new List<Good>(), printCategoriesOfGoods);

        }

        
        public static void ListOfAllGoods(Warehouse allGoods)
        {
            WayOfPrinting(allGoods, (goods) => PrintGoods<Good>(goods, "List of all goods"));
        }

        public static void ListAfetrEditing(Warehouse allGoods)
        {

            WayOfPrinting(allGoods, (goods) => PrintGoods<Good>(goods, "Edited goods"));
        }

        public static void ListOfFindedGoods(Warehouse allGoods)
        {
            WayOfPrinting(allGoods, (goods) => PrintGoods<Good>(goods, "Finded goods"), true);
        }


        private static void PrintInvoice(Invoice invoice, string title)
        {
            invoice.DateOfMakingInvoice = DateTime.Now;

            PrintGoods<Good>(invoice, $"{title} {invoice.NumberOfInvoice}", invoice.DateOfMakingInvoice);
        }

        public static void IncomeInvoice(Invoice invoice)
        {
            PrintInvoice(invoice, "Income invoice");
        }

        public static void ExpenceInvoice(Invoice invoice)
        {
            PrintInvoice(invoice, "Expence invoice");
        }



        private static void PrintInvoices(BaseOfInvoices invoices, string title)//all invoices
        {
            foreach (Invoice invoice in invoices)
            {
                PrintGoods<Good>(invoice, $"{title} {invoice.NumberOfInvoice}", invoice.DateOfMakingInvoice);
            }
        }

        public static void IncomeInvoices(BaseOfInvoices incomeInvoice)
        {
            PrintInvoices(incomeInvoice, "Income invoice");
        }

        public static void ExpenceInvoices(BaseOfInvoices expenceInvoice)
        {
            PrintInvoices(expenceInvoice, "Expence invoice");
        }

       
        public static void Message(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}