using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
                        Invoice invoice = new Invoice();
                        invoice.NumberOfInvoice = invoiceNumber;
                        invoiceList.Add(invoice);
                    }
                    else if (DateTime.TryParse(line, out DateTime date))
                    {
                        invoiceList[invoiceList.Count - 1].DateOfMakingInvoice = date;
                    }
                    else
                    {
                        invoiceList[invoiceList.Count - 1].Add(AddExistingTypeOfGood(line));
                    }
                }

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
                foreach (Invoice invoice in invoices)
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



        public static void CreateAndWriteToFile(Warehouse foundGoods, FindGoods findGoods)
        {
            string folderPath = @"C:\Users\Админ\Desktop\Found goods";

            try
            {
                if (foundGoods.Count != 0)
                {
                    CreateDirectoryIfNotExists(folderPath);

                    string newFileName = GenerateNewFileName(folderPath);

                    string filePath = Path.Combine(folderPath, newFileName);

                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        WriteSearchCharacteristics(writer, findGoods);
                        writer.WriteLine(" ");

                        var table = CreateConsoleTable(foundGoods);
                        WriteGoodsTableToFile(writer, table);

                        writer.WriteLine("\n\n Total sum: {0} uah\n", TotalSum.CalculateTotalSum(foundGoods));
                    }

                    Print.Message(ConsoleColor.DarkYellow, $"\n File \"{newFileName}\" was successfully created, and information about found goods was written.\n");
                }
                else
                {
                    Print.Message(ConsoleColor.Red, $"\n New file was not created, because no goods were found with the specified characteristics.\n");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void CreateDirectoryIfNotExists(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        private static string GenerateNewFileName(string folderPath)
        {
            string[] existingFiles = Directory.GetFiles(folderPath);

            int existingFilesCount = existingFiles.Count(filepath => Path.GetFileNameWithoutExtension(filepath).StartsWith("found goods"));

            return $"found goods {existingFilesCount + 1}.txt";
        }

        private static void WriteSearchCharacteristics(StreamWriter writer, FindGoods findGoods)
        {
            writer.WriteLine("Search was made by these characteristics:");

            foreach (PropertyInfo property in typeof(FindGoods).GetProperties())
            {
                object value = property.GetValue(findGoods)!;
                object defaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType)! : "";

                if (!value.Equals(defaultValue))
                {
                    writer.WriteLine($"{property.Name}: {value}");
                }
            }
        }

        private static ConsoleTable CreateConsoleTable(Warehouse foundGoods)
        {
            var table = new ConsoleTable("№", "Category", "Name of a good", "Size", "Color", "Brand", "Model", "Company",
                "Unit of measure", "Unit of price", "Amount", "Expiry date", "Date of last delivery");

            int counter = 1;
            foreach (Good item in foundGoods)
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
            }

            return table;
        }

        private static void WriteGoodsTableToFile(StreamWriter writer, ConsoleTable table)
        {
            writer.WriteLine("\n\t\t\t\t\t\t\t\tFound goods\n");
            writer.WriteLine(table.ToString());
        }


    }
}
