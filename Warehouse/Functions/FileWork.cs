using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class FileWork
    {
        private static readonly string filePathToGoods = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Database\Goods.txt";
        private static readonly string filePathToIncomeInvoices = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Database\IncomeInvoices.txt";
        private static readonly string filePathToExpenceInvoices = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Database\ExpenceInvoices.txt";

        private static void AddExistingGoods(Warehouse allGoods)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePathToGoods);

                foreach (string line in lines)
                {
                    allGoods.Add(AddExistingTypeOfGood(line));
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            }
        }

        private static void RewriteGoodsInFile(Warehouse allGoods)
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
                return string.Join(",", food.Category, food.NameOfGood, food.UnitOfMeasure, FormatUnitPrice(food.UnitPrice), food.Amount, food.ExpiryDate, food.DateOfLastDelivery);
            }
            else if (good is Clothing clothing)
            {
                return string.Join(",", clothing.Category, clothing.NameOfGood, clothing.Size, clothing.Color, clothing.Brand, clothing.UnitOfMeasure, FormatUnitPrice(clothing.UnitPrice), clothing.Amount, clothing.DateOfLastDelivery);
            }
            else if (good is Electronics electronics)
            {
                return string.Join(",", electronics.Category, electronics.NameOfGood, electronics.Model, electronics.Company, electronics.UnitOfMeasure, FormatUnitPrice(electronics.UnitPrice), electronics.Amount, electronics.DateOfLastDelivery);
            }
            else
            {
                throw new ArgumentException("Unsupported type of Good");
            }
        }

        private static string FormatUnitPrice(double unitPrice)
        {
            return unitPrice.ToString(CultureInfo.InvariantCulture);
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
                        invoiceList[invoiceList.Count - 1].Add(AddExistingTypeOfGood(line));
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
                Print.Message(ConsoleColor.Red, $"An error occurred while reading from file: {ex.Message}");
            }
        }

        private static void AddExistingIncomeInvoices(BaseOfInvoices invoices)
        {
            AddExistingInvoices(invoices, filePathToIncomeInvoices);
        }

        private static void AddExistingExpenceInvoices(BaseOfInvoices invoices)
        {
            AddExistingInvoices(invoices, filePathToExpenceInvoices);
        }



        private static void RewriteInvoices(BaseOfInvoices invoices, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath, false))
            {
                foreach(Invoice invoice in invoices)
                {
                    writer.WriteLine(invoice.NumberOfInvoice);
                    writer.WriteLine(invoice.DateOfMakingInvoice);

                    foreach (Good good in invoice)
                    {
                        writer.WriteLine(FormatGood(good));
                    }
                }
                
            }
        }

        private static void RewriteIncomeInvoices(BaseOfInvoices incomeInvoices)
        {
            RewriteInvoices(incomeInvoices, filePathToIncomeInvoices);
        }

        private static void RewriteExpenceInvoices(BaseOfInvoices expenceInvoices)
        {
            RewriteInvoices(expenceInvoices, filePathToExpenceInvoices);
        }


        private static Good AddExistingTypeOfGood(string line)
        {
            string[] values = line.Split(',');

            switch (values[0])
            {
                case "food":
                    return new Food(values[0], values[1], values[2], double.Parse(values[3], CultureInfo.InvariantCulture), int.Parse(values[4]), DateTime.Parse(values[5]), DateTime.Parse(values[6]));
                case "clothing":
                    return new Clothing(values[0], values[1], values[2], values[3], values[4], values[5], double.Parse(values[6], CultureInfo.InvariantCulture), int.Parse(values[7]), DateTime.Parse(values[8]));
                case "electronics":
                    return new Electronics(values[0], values[1], values[2], values[3], values[4], double.Parse(values[5], CultureInfo.InvariantCulture), int.Parse(values[6]), DateTime.Parse(values[7]));
                default:
                    throw new ArgumentException("Unsupported type of Good");
            }
        }


        public static void DownloadData(Warehouse goods, BaseOfInvoices incomeInvoices, BaseOfInvoices expenceInvoices)
        {
            AddExistingGoods(goods);
            AddExistingIncomeInvoices(incomeInvoices);
            AddExistingExpenceInvoices(expenceInvoices);
        }

        public static void UploadData(Warehouse goods, BaseOfInvoices incomeInvoices, BaseOfInvoices expenceInvoices)
        {
            RewriteGoodsInFile(goods);
            RewriteIncomeInvoices(incomeInvoices);
            RewriteExpenceInvoices(expenceInvoices);
        }


        public static void SaveFoundGoods(Warehouse foundGoods)
        {
            string folderPath = @"C:\Users\Админ\Desktop\Знайдені товари";

            List<string> foundProducts = new List<string>();

            foreach (Good good in foundGoods)
            {
                foundProducts.Add(FormatGood(good));
            }

            CreateAndWriteToFile(folderPath, foundProducts);
        }

        private static void CreateAndWriteToFile(string folderPath, List<string> foundProducts)
        {
            try
            {
                if(foundProducts.Count != 0)
                {
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string[] existingFiles = Directory.GetFiles(folderPath);

                    string fileName;
                    int existingFilesCount = 0;

                    foreach (string filepath in existingFiles)
                    {
                        fileName = Path.GetFileNameWithoutExtension(filepath);
                        if (fileName.StartsWith("found goods"))
                        {
                            existingFilesCount++;
                        }
                    }

                    string newFileName = $"found goods {existingFilesCount + 1}.txt";

                    string filePath = Path.Combine(folderPath, newFileName);
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (string product in foundProducts)
                        {
                            writer.WriteLine(product);
                        }
                    }

                    Print.Message(ConsoleColor.DarkYellow, $"\n File \"{newFileName}\" was succesfully created and information about found goods was already written.\n");
                }
                else
                {
                    Print.Message(ConsoleColor.Red, $"\n New file wasn't created, because no goods were found by these characteristics.\n");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mistake occured: {ex.Message}");
            }
        }

    }
}
