namespace Warehouse
{
    internal class Electronics : Good
    {
        public string Model { get; set; }
        public string Company { get; set; }

        public Electronics(string category, string nameOfGood, string model, string company, string unitOfMeasure, double unitPrice, int amount, DateTime dateOfLastDelivery)
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            Model = model;
            Company = company;
        }
        public Electronics(Electronics other) : base(other)
        {
            Model = other.Model;
            Company = other.Company;
        }

        public static void EditElectronicsCharacteristics(Electronics electronics)
        {
            Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
        "1. Name of a good\n2. Model\n3. Company\n4. Unit of measure\n5. Unit of price\n6. Amount\n7. Date of last delivery\n");
             List<int> characterList = Validator.GetTheValidationCharacteristicsForEditingElectronics("Enter the number / numbers of characteristic / characteristics that you want to change: \n");

            var characteristics = characterList.OrderBy(x => x);
            foreach (int item in characteristics)
            {
                switch (item)
                {
                    case 1:
                        electronics.NameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the good's name: ");
                        break;
                    case 2:
                        electronics.Model = Validator.GetTheValidationModel("Enter the name of the model of the good: ");
                        break;
                    case 3:
                        electronics.Company = Validator.GetTheValidationGoodCharacteristic("Enter the name of the company that produces this good: ");
                        break;
                    case 4:
                        electronics.UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Enter unit of measure of a good: ");
                        break;
                    case 5:
                        electronics.UnitPrice = Validator.GetTheValidationUnitPrice("Enter unit of price of a good: ");
                        break;
                    case 6:
                        electronics.Amount = Validator.GetTheValidationAmount("Enter amount of delivered goods: ");
                        break;
                    case 7:
                        electronics.DateOfLastDelivery = Validator.GetTheValidationDateTime("Enter date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                        break;
                }
            }
        }
    }
}
