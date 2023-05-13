namespace Warehouse
{
    public class Food : Good
    {
        public DateTime ExpiryDate { get; set; }

        public Food(string category, string nameOfGood, string unitOfMeasure, double unitPrice, int amount, DateTime expiryDate, DateTime dateOfLastDelivery)
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            ExpiryDate = expiryDate;
        }
        public Food(Food other) : base(other)
        {
            ExpiryDate = other.ExpiryDate;
        }

        public static void EditFoodCharacteristics(Food food)
        {
            Console.WriteLine("\nThere are all the characteristics of food goods that you can change:\n" +
        "1. Name of a good\n2. Unit of measure\n3. Unit of price\n4. Amount\n5. Expiry date\n6. Date of last delivery\n");

            List<int> characterList = Validator.GetTheValidationCharacteristicsForEditingFood("Enter the number / numbers of characteristic / characteristics that you want to change: \n");
            var characteristics = characterList.OrderBy(x => x);

            foreach (int item in characteristics)
            {
                switch (item)
                {
                    case 1:
                        food.NameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the good's name: ");
                        break;
                    case 2:
                        food.UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Enter unit of measure of a good: ");
                        break;
                    case 3:
                        food.UnitPrice = Validator.GetTheValidationUnitPrice("Enter unit of price of a good: ");
                        break;
                    case 4:
                        food.Amount = Validator.GetTheValidationAmount("Enter amount of delivered goods: ");
                        break;
                    case 5:
                        food.ExpiryDate = Validator.GetTheValidationInput("Enter expiry date of this good (in format dd.mm.yyyy): ", DateTime.Parse);
                        break;
                    case 6:
                        food.DateOfLastDelivery = Validator.GetTheValidationDateTime("Enter date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                        break;
                }
            }
        }
    }
}
