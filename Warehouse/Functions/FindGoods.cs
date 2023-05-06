using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{ 
    public class FindGoods
    {
        public string? Category { get; set; }
        public string? NameOfGood { get; set; }
        public string? UnitOfMeasure { get; set; }
        public double UnitPriceFrom { get; set; }
        public double UnitPriceTo { get; set; }
        public int AmountFrom { get; set; }
        public int AmountTo { get; set; }
        public DateTime DateOfLastDeliveryFrom { get; set; }
        public DateTime DateOfLastDeliveryTo { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }
        public DateTime ExpiryDateFrom { get; set; }
        public DateTime ExpiryDateTo { get; set; }

        public void CharacteristicsForFindingGoods()
        {
            Console.WriteLine("\nChoose the type of product out of these:\n-Food\n-Clothing\n-Electronics");
            Category = Validator.GetTheValidationType("\nEnter the name of the type of the product: ", allowNullInput: true);
            NameOfGood = Validator.GetTheValidationGoodCharacteristic("Enter the name of the good: ", allowNullInput: true);
            UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Write unit of measure of a good: ", allowNullInput: true);
            UnitPriceFrom = Validator.GetTheValidationUnitPrice("Write unit of price of a good(from): ", allowNullInput: true);
            UnitPriceTo = Validator.GetTheValidationUnitPrice("Write unit of price of a good(to): ", allowNullInput: true);
            AmountFrom = Validator.GetTheValidationAmount("Write amount of delivered goods(from): ", allowNullInput: true);
            AmountTo = Validator.GetTheValidationInput("Write amount of delivered goods(to): ", int.Parse, allowNullInput: true);
            DateOfLastDeliveryFrom = Validator.GetTheValidationDateTime("Write date and time of last delivery of a good (in format dd.mm.yyyy hh:mm:ss)(from): ", allowNullInput: true);
            DateOfLastDeliveryTo = Validator.GetTheValidationDateTime("Enter date and time of last delivery of a good (in the format dd.mm.yyyy)(to): ", allowNullInput: true);
            ExpiryDateFrom = Validator.GetTheValidationInput("Enter an expiry date of a product (in the format dd.mm.yyyy)(from):  ", DateTime.Parse, allowNullInput: true);
            ExpiryDateTo = Validator.GetTheValidationInput("Enter an expiry date of a product (in the format dd.mm.yyyy)(to): ", DateTime.Parse, allowNullInput: true);
            Size = Validator.GetTheValidationSize("Enter the size of the product: ", allowNullInput: true);
            Color = Validator.GetTheValidationGoodCharacteristic("Enter the color of the product: ", allowNullInput: true);
            Brand = Validator.GetTheValidationGoodCharacteristic("Enter the name of the brand of the product: ", allowNullInput: true);
            Model = Validator.GetTheValidationGoodCharacteristic("Enter the name of the model of the product: ", allowNullInput: true);
            Company = Validator.GetTheValidationGoodCharacteristic("Enter the name of the company that produces this product: ", allowNullInput: true);
        }

    }
}
