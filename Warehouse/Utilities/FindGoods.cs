using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class FindGoods : IEnumerable<object>
    {
        public string Category { get; set; }
        public string NameOfGood { get; set; }
        public string UnitOfMeasure { get; set; }
        public double UnitPriceFrom { get; set; }
        public double UnitPriceTo { get; set; }
        public int AmountFrom { get; set; }
        public int AmountTo { get; set; }
        public DateTime DateOfLastDeliveryFrom { get; set; }
        public DateTime DateOfLastDeliveryTo { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Company { get; set; }
        public DateTime ExpiryDateFrom { get; set; }
        public DateTime ExpiryDateTo { get; set; }

        public FindGoods()
        {
            Category = "";
            NameOfGood = "";
            UnitOfMeasure = "";
            UnitPriceFrom = 0;
            UnitPriceTo = 0;
            AmountFrom = 0;
            AmountTo = 0;
            DateOfLastDeliveryFrom = DateTime.MinValue;
            DateOfLastDeliveryTo = DateTime.MinValue;
            Size = "";
            Color = "";
            Brand = "";
            Model = "";
            Company = "";
            ExpiryDateFrom = DateTime.MinValue;
            ExpiryDateTo = DateTime.MinValue;
        }

        public IEnumerator<object> GetEnumerator()
        {
            yield return Category!;
            yield return NameOfGood!;
            yield return UnitOfMeasure!;
            yield return UnitPriceFrom;
            yield return UnitPriceTo;
            yield return AmountFrom;
            yield return AmountTo;
            yield return DateOfLastDeliveryFrom;
            yield return DateOfLastDeliveryTo;
            yield return Size!;
            yield return Color!;
            yield return Brand!;
            yield return Model!;
            yield return Company!;
            yield return ExpiryDateFrom;
            yield return ExpiryDateTo;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CharacteristicsForFindingGoods()
        {
            Print.Message(ConsoleColor.Yellow, "\n\nSearch options:\n\n" +
            "1. By product type name\n" +
            "2. By good's name\n" +
            "3. By unit of measure of a good\n" +
            "4. By unit of price of a good (from)\n" +
            "5. By unit of price of a good (to)\n" +
            "6. By amount of delivered goods (from)\n" +
            "7. By amount of delivered goods (to)\n" +
            "8. By date and time of last delivery of a good (in format dd.mm.yyyy hh:mm:ss) (from)\n" +
            "9. By date and time of last delivery of a good (in the format dd.mm.yyyy) (to)\n" +
            "10.By expiry date of a good (in the format dd.mm.yyyy) (from)" +
            "11.By expiry date of a good (in the format dd.mm.yyyy) (to)" +
            "12.By the size of the good\n" +
            "13.By the color of the product\n" +
            "14.By the name of the brand of the good\n" +
            "15.By the name of the model of the good\n" +
            "16.By the name of the company that produces this good");

            List<int> searchOptions = Validator.GetTheValidationCharacteristicsForFindingGoods("\nEnter the number / numbers of options to be used for the search: ");

            var orderedSearchOptions = searchOptions.OrderBy(x => x);

            foreach (int item in orderedSearchOptions)
            {
                switch(item)
                {
                    case 1:
                        Console.WriteLine("\nChoose the type of product out of these:\n-Food\n-Clothing\n-Electronics");
                        Category = Validator.GetTheValidationType("\nEnter the product type name: ");
                        break;
                    case 2:
                        NameOfGood = Validator.GetTheValidationGoodCharacteristic("Enter the good's name: ");
                        break;
                    case 3:
                        UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Enter unit of measure of a good: ");
                        break;
                    case 4:
                        UnitPriceFrom = Validator.GetTheValidationUnitPrice("Enter unit of price of a good (from): ");
                        break;
                    case 5:
                        UnitPriceTo = Validator.GetTheValidationUnitPrice("Enter unit of price of a good (to): ");
                        break;
                    case 6:
                        AmountFrom = Validator.GetTheValidationAmount("Enter amount of delivered goods (from): ");
                        break;
                    case 7:
                        AmountTo = Validator.GetTheValidationInput("Enter amount of delivered goods (to): ", int.Parse);
                        break;
                    case 8:
                        DateOfLastDeliveryFrom = Validator.GetTheValidationDateTime("Enter date and time of last delivery of a good (in format dd.mm.yyyy hh:mm:ss) (from): ");
                        break;
                    case 9:
                        DateOfLastDeliveryTo = Validator.GetTheValidationDateTime("Enter date and time of last delivery of a good (in the format dd.mm.yyyy) (to): ");
                        break;
                    case 10:
                        ExpiryDateFrom = Validator.GetTheValidationInput("Enter an expiry date of a good (in the format dd.mm.yyyy) (from):  ", DateTime.Parse);
                        break;
                    case 11:
                        ExpiryDateTo = Validator.GetTheValidationInput("Enter an expiry date of a good (in the format dd.mm.yyyy) (to): ", DateTime.Parse);
                        break;
                    case 12:
                        Size = Validator.GetTheValidationSize("Enter the size of the good: ");
                        break;
                    case 13:
                        Color = Validator.GetTheValidationGoodCharacteristic("Enter the color of the product: ");
                        break;
                    case 14:
                        Brand = Validator.GetTheValidationGoodCharacteristic("Enter the name of the brand of the good: ");
                        break;
                    case 15:
                        Model = Validator.GetTheValidationModel("Enter the name of the model of the good: ");
                        break;
                    case 16:
                        Company = Validator.GetTheValidationGoodCharacteristic("Enter the name of the company that produces this good: ");
                        break;
                }
            }

        }

    }
}
