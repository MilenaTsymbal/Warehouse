using System.Drawing;

namespace Warehouse
{
    internal class Clothing : Good
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public Clothing(string category, string nameOfGood, string size, string color, string brand, string unitOfMeasure, double unitPrice, int amount, DateTime dateOfLastDelivery)
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            Size = size;
            Color = color;
            Brand = brand;
        }
        public Clothing(Clothing other) : base(other)
        {
            Size = other.Size;
            Color = other.Color;
            Brand = other.Brand;
        }

        public static void EditClothingCharacteristics(Clothing clothing)
        {
            Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
       "1. Name of a good\n2. Size\n3. Color\n4. Brand\n5. Unit of measure\n6. Unit of price\n7. Amount\n8. Date of last delivery\n");
            List<int> characterList = Validator.GetTheValidationCharacteristicsForEditingClothing("Enter the number / numbers of characteristic / characteristics that you want to change: \n");

            var characteristics = characterList.OrderBy(x => x);
            foreach (int item in characteristics)
            {
                switch (item)
                {
                    case 1:
                        clothing.NameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the good's name: ");
                        break;
                    case 2:
                        clothing.Size = Validator.GetTheValidationSize("Enter the size of the good: ");
                        break;
                    case 3:
                        clothing.Color = Validator.GetTheValidationGoodCharacteristic("Enter the color of the good: ");
                        break;
                    case 4:
                        clothing.Brand = Validator.GetTheValidationGoodCharacteristic("Enter the name of the brand of the good: ");
                        break;
                    case 5:
                        clothing.UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Enter unit of measure of a good: ");
                        break;
                    case 6:
                        clothing.UnitPrice = Validator.GetTheValidationUnitPrice("Enter unit of price of a good: ");
                        break;
                    case 7:
                        clothing.Amount = Validator.GetTheValidationAmount("Enter amount of delivered goods: ");
                        break;
                    case 8:
                        clothing.DateOfLastDelivery = Validator.GetTheValidationDateTime("Enter date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                        break;
                }
            }
        }
    }
}
