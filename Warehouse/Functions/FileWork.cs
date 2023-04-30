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
        private static string filePathToGoods = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Goods\Goods.txt";
        private static string filePathToIncomeInvoices = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Invoices\IncomeInvoices.txt";
        private static string filePathToExpenceInvoices = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Invoices\ExpenceInvoices.txt";
        public static void AddExistingGoods(Warehouse allGoods)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePathToGoods);

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    switch (values[0])
                    {
                        case "food":
                        case "drinks":
                            allGoods.Add(new Food(values[0], values[1], values[2], int.Parse(values[3]), int.Parse(values[4]), DateTime.Parse(values[5]), DateTime.Parse(values[6])));
                            break;
                        case "clothes":
                        case "footwear":
                            allGoods.Add(new Clothing(values[0], values[1], values[2], values[3], values[4], values[5], int.Parse(values[6]), int.Parse(values[7]), DateTime.Parse(values[8])));
                            break;
                        case "electronics":
                            allGoods.Add(new Electronics(values[0], values[1], values[2], values[3], values[4], int.Parse(values[5]), int.Parse(values[6]), DateTime.Parse(values[7])));
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
            int lastLineNumber = File.ReadLines(filePathToGoods).Count();
            string arrayString = "";

            using (StreamWriter writer = new StreamWriter(filePathToGoods, true))
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

            using (StreamWriter writer = new StreamWriter(filePathToGoods, false))
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

        private static void AddExistingInvoices(BaseOfInvoices invoices, string filepath)
        {
            try
            {
                string[] fileLines = File.ReadAllLines(filepath);
                BaseOfInvoices invoiceList = new BaseOfInvoices();

                foreach (string line in fileLines)
                {
                    if (int.TryParse(line, out int invoiceNumber))
                    {
                        // Если строка начинается с числа, то это номер счета-фактуры
                        Invoice invoice = new Invoice();
                        invoice.NumberOfInvoice = invoiceNumber;
                        invoiceList.Add(invoice);
                    }
                    else if (DateTime.TryParse(line, out DateTime date))
                    {
                        // Если строка является датой, то это дата создания счета-фактуры
                        invoiceList[invoiceList.Count - 1].DateOfMakingInvoice = date;
                    }
                    else
                    {
                        // Иначе это данные о товаре в счете-фактуре
                        string[] values = line.Split(',');

                        switch (values[0])
                        {
                            case "food":
                            case "drinks":
                                invoiceList[invoiceList.Count - 1].Add(new Food(values[0], values[1], values[2], int.Parse(values[3]), int.Parse(values[4]), DateTime.Parse(values[5]), DateTime.Parse(values[6])));
                                break;
                            case "clothes":
                            case "footwear":
                                invoiceList[invoiceList.Count - 1].Add(new Clothing(values[0], values[1], values[2], values[3], values[4], values[5], int.Parse(values[6]), int.Parse(values[7]), DateTime.Parse(values[8])));
                                break;
                            case "electronics":
                                invoiceList[invoiceList.Count - 1].Add(new Electronics(values[0], values[1], values[2], values[3], values[4], int.Parse(values[5]), int.Parse(values[6]), DateTime.Parse(values[7])));
                                break;
                        }
                    }
                }

                // Добавляем все накладные в базу
                foreach (Invoice invoice in invoiceList)
                {
                    invoices.Add(invoice);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading from file: {ex.Message}");
            }
        }

        public static void AddExistingIncomeInvoices(BaseOfInvoices invoices)
        {
            AddExistingInvoices(invoices, filePathToIncomeInvoices);
        }
        public static void AddExistingExpenceInvoices(BaseOfInvoices invoices)
        {
            AddExistingInvoices(invoices, filePathToExpenceInvoices);
        }


        private static void AddNewInvoice(Invoice invoice, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(invoice.NumberOfInvoice);
                writer.WriteLine(invoice.DateOfMakingInvoice);

                foreach (Good good in invoice)
                {
                    if (good is Food food)
                    {
                        writer.WriteLine(string.Join(",", food.Category, food.NameOfGood, food.UnitOfMeasure, food.UnitPrice, food.Amount, food.ExpiryDate, food.DateOfLastDelivery));
                    }
                    else if (good is Clothing clothing)
                    {
                        writer.WriteLine(string.Join(",", clothing.Category, clothing.NameOfGood, clothing.Size, clothing.Color, clothing.Brand, clothing.UnitOfMeasure, clothing.UnitPrice, clothing.Amount, clothing.DateOfLastDelivery));
                    }
                    else if (good is Electronics electronics)
                    {
                        writer.WriteLine(string.Join(",", electronics.Category, electronics.NameOfGood, electronics.Model, electronics.Company, electronics.UnitOfMeasure, electronics.UnitPrice, electronics.Amount, electronics.DateOfLastDelivery));
                    }
                }

            }
        }

        public static void AddNewIncomeInvoice(Invoice incomeInvoice)
        {
            AddNewInvoice(incomeInvoice, filePathToIncomeInvoices);
        }
        public static void AddNewExpenceInvoice(Invoice expenceInvoice)
        {
            AddNewInvoice(expenceInvoice, filePathToExpenceInvoices);
        }


        private static int CountNumericLines(string filePath)
        {
            int count = 0;
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                bool isNumeric = true;
                foreach (char c in line)
                {
                    if (!Char.IsDigit(c))
                    {
                        isNumeric = false;
                        break;
                    }
                }
                if (isNumeric)
                {
                    count++;
                }
            }

            return count;
        }

        public static int CountIncomeInvoices()
        {
            return CountNumericLines(filePathToIncomeInvoices);
        }

        public static int CountExpenceInvoices()
        {
            return CountNumericLines(filePathToExpenceInvoices);
        }
    }
}
