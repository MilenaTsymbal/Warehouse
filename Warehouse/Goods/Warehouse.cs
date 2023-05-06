using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace Warehouse
{
    public class Warehouse : List<Good>
    {
        public List<Good?> goods;

        public Warehouse()
        {
            goods = new List<Good?>();
        }


        public void AddNewGoods(BaseOfInvoices incomeInvoices)
        {
            Invoice addedGoods = new Invoice();
            addedGoods.NumberOfInvoice = FileWork.CountIncomeInvoices() + 1;
            int lastItem = Count;

            int numberOfNewProducts = Validator.GetTheValidationInput("\nEnter the number of goods you want to add: ", int.Parse);

            if (numberOfNewProducts > 0)
            {
                for (int i = 1; i <= numberOfNewProducts; i++)
                {
                    Good product = CreateNewGood();
                    Add(product);
                    addedGoods.Add(product);
                    Console.WriteLine();
                }
                Console.WriteLine();

                FileWork.AddNewGoodsToFile(this);
                FileWork.AddNewIncomeInvoice(addedGoods);
                incomeInvoices.Add(addedGoods);
                Print.IncomeInvoice(addedGoods);
            }
            else
            {
                Print.Message(ConsoleColor.Red, "\nAn invalid value, the number must be greater than 0");
                AddNewGoods(incomeInvoices);
            }

        }

        private Good CreateNewGood()
        {
            Console.WriteLine("\nChoose the type of product out of these:\n-Food\n-Clothing\n-Electronics");
            string category = Validator.GetTheValidationType("\nEnter the name of the type of the product: ");

            string nameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the name of the good: ");

            string unitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Write unit of measure of a good: ");

            double unitPrice = Validator.GetTheValidationUnitPrice("Write unit of price of a good: ");

            int amount = Validator.GetTheValidationAmount("Write amount of delivered goods: ");

            DateTime dateOfLastDelivery = Validator.GetTheValidationDateTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");

            switch (category)
            {
                case "food":
                case "drinks":
                    DateTime expiryDate = Validator.GetTheValidationInput("Enter an expiry date of a product (in the format dd.mm.yyyy):  ", DateTime.Parse);
                    return new Food(category, nameOfGood, unitOfMeasure, unitPrice, amount, expiryDate, dateOfLastDelivery);
                case "clothing":
                case "footwear":
                    string size = Validator.GetTheValidationSize("Enter the size of the product: ");
                    string color = Validator.GetTheValidationGoodCharacteristic("Enter the color of the product: ");
                    string brand = Validator.GetTheValidationGoodCharacteristic("Enter the name of the brand of the product: ");
                    return new Clothing(category, nameOfGood, size, color, brand, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);
                case "electronics":
                    string model = Validator.GetTheValidationGoodCharacteristic("Enter the name of the model of the product: ");
                    string company = Validator.GetTheValidationGoodCharacteristic("Enter the name of the company that produces this product: ");
                    return new Electronics(category, nameOfGood, model, company, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);
                default:
                    throw new InvalidOperationException("Invalid category: " + category);
            }
        }



        public void EditGood()
        {
            Print.PrintGoods<Good>(this, "List of all goods");

            Warehouse editedGoods = new Warehouse();

            int amountOfGoodsForChange = Validator.GetTheValidationNumberOfGoods("\nEnter the number of goods that will be changed: ");

            if(amountOfGoodsForChange > 0)
            {
                for (int i = 0; i < amountOfGoodsForChange; i++)
                {
                    int indexOfGood = Validator.GetTheValidationNumberOfGoods($"\nEnter the number of a good ({i + 1}/{amountOfGoodsForChange}) that you want to change: ");

                    EditGoodCharacteristics(indexOfGood);
                    editedGoods.Add(this[indexOfGood - 1]);
                }

                FileWork.RewriteGoodsInFile(this);
                Print.ListAfetrEditing(editedGoods);
            }
            else
            {
                Print.Message(ConsoleColor.Red, "\nAn invalid value, the number must be greater than 0");
                EditGood();
            }
           
        }

        private void EditGoodCharacteristics(int indexOfGood)
        {
            List<int> characterList = new List<int>();

            if (this[indexOfGood - 1] is Food food)
            {
                Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
                "1. Name of a good\n2. Unit of measure\n3. Unit of price\n4. Amount\n5. Expiry date\n6. Date of last delivery\n");
                characterList = Validator.GetTheValidationListOfCharacteristics("Enter the number / numbers of characteristic / characteristics that you want to change: \n");
                
                var characteristics = characterList.OrderBy(x => x);

                foreach (int item in characteristics)
                {
                    switch (item)
                    {
                        case 1:
                            food.NameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the name of the good: ");
                            break;
                        case 2:
                            food.UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Write unit of measure of a good: ");
                            break;
                        case 3:
                            food.UnitPrice = Validator.GetTheValidationUnitPrice("Write unit of price of a good: ");
                            break;
                        case 4:
                            food.Amount = Validator.GetTheValidationAmount("Write amount of delivered goods: ");
                            break;
                        case 5:
                            food.ExpiryDate = Validator.GetTheValidationInput("Write expiry date of this good (in format dd.mm.yyyy): ", DateTime.Parse);
                            break;
                        case 6:
                            food.DateOfLastDelivery = Validator.GetTheValidationDateTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;
                    }
                }
            }
            else if (this[indexOfGood - 1] is Clothing clothing)
            {
                Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
       "1. Name of a good\n2. Size\n3. Brand\n4. Unit of measure\n5. Unit of price\n6. Amount\n7. Date of last delivery\n");
                characterList = Validator.GetTheValidationListOfCharacteristics("Enter the number / numbers of characteristic / characteristics that you want to change: \n");

                var characteristics = characterList.OrderBy(x => x);
                foreach (int item in characteristics)
                {
                    switch (item)
                    {
                        case 1:
                            clothing.NameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the name of the good: ");
                            break;
                        case 2:
                            clothing.Size = Validator.GetTheValidationSize("Enter the size of the product: ");
                            break;
                        case 3:
                            clothing.Brand = Validator.GetTheValidationGoodCharacteristic("Enter the name of the brand of the product: ");
                            break;
                        case 4:
                            clothing.UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Write unit of measure of a good: ");
                            break;
                        case 5:
                            clothing.UnitPrice = Validator.GetTheValidationUnitPrice("Write unit of price of a good: ");
                            break;
                        case 6:
                            clothing.Amount = Validator.GetTheValidationAmount("Write amount of delivered goods: ");
                            break;
                        case 7:
                            clothing.DateOfLastDelivery = Validator.GetTheValidationDateTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;
                    }
                }
            }
            else if (this[indexOfGood - 1] is Electronics electronics)
            {
                Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
        "1. Name of a good\n2. Model\n3. Company\n4. Unit of measure\n5. Unit of price\n6. Amount\n7. Date of last delivery\n");
                characterList = Validator.GetTheValidationListOfCharacteristics("Enter the number / numbers of characteristic / characteristics that you want to change: \n");

                var characteristics = characterList.OrderBy(x => x);
                foreach (int item in characteristics)
                {
                    switch (item)
                    {
                        case 1:
                            electronics.NameOfGood = Validator.GetTheValidationGoodCharacteristic("\nEnter the name of the good: ");
                            break;
                        case 2:
                            electronics.Model = Validator.GetTheValidationGoodCharacteristic("Enter the name of the model of the product: ");
                            break;
                        case 3:
                            electronics.Company = Validator.GetTheValidationGoodCharacteristic("Enter the name of the company that produces this product: ");
                            break;
                        case 4:
                            electronics.UnitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Write unit of measure of a good: ");
                            break;
                        case 5:
                            electronics.UnitPrice = Validator.GetTheValidationUnitPrice("Write unit of price of a good: ");
                            break;
                        case 6:
                            electronics.Amount = Validator.GetTheValidationAmount("Write amount of delivered goods: ");
                            break;
                        case 7:
                            electronics.DateOfLastDelivery = Validator.GetTheValidationDateTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;
                    }
                }
            }
        }



        public void DeleteGoods(BaseOfInvoices expenceInvoices)
        {
            Print.PrintGoods<Good>(this, "List of all goods");

            Invoice deletedGoods = new Invoice();
            deletedGoods.NumberOfInvoice = FileWork.CountExpenceInvoices() + 1;

            int amountOfGoodsForDeletion = Validator.GetTheValidationNumberOfGoods("\nEnter numbers of goods that will be deleted: ");

            if(amountOfGoodsForDeletion > 0)
            {
                for (int i = 0; i < amountOfGoodsForDeletion; i++)
                {
                    int indexOfGood = Validator.GetTheValidationNumberOfGoods($"\nEnter the number of a good ({i + 1}/{amountOfGoodsForDeletion}) that you want to change: ");
                    int amountForDeletion = Validator.GetTheValidationAmountForDeletion($"\nEnter the number of amount of a good ({i + 1}/{amountOfGoodsForDeletion}) that will be transferred: ", this[indexOfGood - 1]);

                    if (amountForDeletion < this[indexOfGood - 1].Amount)
                    {
                        deletedGoods.Add(CreateTempItem(this[indexOfGood - 1]));
                        UpdateAmountOfGoods(indexOfGood, amountForDeletion);
                        UpdateAmountOfDeletedGoods(deletedGoods, amountForDeletion);
                    }
                    else if (amountForDeletion == this[indexOfGood - 1].Amount)
                    {
                        deletedGoods.Add(this[indexOfGood - 1]);
                        RemoveAt(indexOfGood - 1);
                    }
                }

                expenceInvoices.Add(deletedGoods);
                Print.ExpenceInvoice(deletedGoods);
                FileWork.AddNewExpenceInvoice(deletedGoods);
                FileWork.RewriteGoodsInFile(this);
            }
            else
            {
                Print.Message(ConsoleColor.Red, "\nAn invalid value, the number must be greater than 0");
                DeleteGoods(expenceInvoices);
            }
        }

        private Good CreateTempItem(Good good)
        {
            if (good is Food food)
            {
                return new Food(food);
            }
            else if (good is Clothing clothing)
            {
                return new Clothing(clothing);
            }
            else if (good is Electronics electronics)
            {
                return new Electronics(electronics);
            }
            else
            {
                throw new ArgumentException("Invalid type of Good");
            }
        }

        private void UpdateAmountOfGoods(int indexOfGood, int amountForDeletion)
        {
            this[indexOfGood - 1].Amount -= amountForDeletion;
        }

        private void UpdateAmountOfDeletedGoods(Warehouse deletedGoods, int amountForDeletion)
        {
            deletedGoods[deletedGoods.Count - 1].Amount = amountForDeletion;
        }



        public void FindGoods()
        {
            Warehouse foundGoods = new Warehouse();

            FindGoods findGoods = new FindGoods();
            findGoods.CharacteristicsForFindingGoods();

            foreach (Good good in this)
            {
                if (IsGoodMatchingCriteria(good, findGoods))
                {
                    foundGoods.Add(good);
                }
            }
            Print.ListOfFindedGoods(foundGoods);
        }
        
        private bool IsGoodMatchingCriteria(Good good, FindGoods findGoods)
        {
            if ((findGoods.NameOfGood == "" || good.NameOfGood!.ToLower().Contains(findGoods.NameOfGood!.ToLower()))
                && (findGoods.Category == "" || good.Category!.ToLower().Contains(findGoods.Category!.ToLower()))
                && (findGoods.UnitOfMeasure == "" || good.UnitOfMeasure!.ToLower().Contains(findGoods.UnitOfMeasure!.ToLower()))
                && (findGoods.UnitPriceFrom == 0 || findGoods.UnitPriceFrom <= good.UnitPrice)
                && (findGoods.UnitPriceTo == 0 || findGoods.UnitPriceTo >= good.UnitPrice)
                && (findGoods.AmountFrom == 0 || findGoods.AmountFrom <= good.Amount)
                && (findGoods.AmountTo == 0 || findGoods.AmountTo >= good.Amount)
                && (findGoods.DateOfLastDeliveryFrom == DateTime.MinValue || findGoods.DateOfLastDeliveryFrom <= good.DateOfLastDelivery)
                && (findGoods.DateOfLastDeliveryTo == DateTime.MinValue || findGoods.DateOfLastDeliveryTo >= good.DateOfLastDelivery)
                && (findGoods.ExpiryDateFrom == DateTime.MinValue || (good is Food foodExpiryDateFrom && findGoods.ExpiryDateFrom <= foodExpiryDateFrom.ExpiryDate))
                && (findGoods.ExpiryDateTo == DateTime.MinValue || (good is Food foodExpiryDateTo && findGoods.ExpiryDateTo >= foodExpiryDateTo.ExpiryDate))
                && (findGoods.Size == "" || (good is Clothing clothingSize && clothingSize.Size!.ToLower().Contains(findGoods.Size!.ToLower())))
                && (findGoods.Color == "" || (good is Clothing clothingColor && clothingColor.Color!.ToLower().Contains(findGoods.Color!.ToLower())))
                && (findGoods.Brand == "" || (good is Clothing clothingBrand && clothingBrand.Brand!.ToLower().Contains(findGoods.Brand!.ToLower())))
                && (findGoods.Model == "" || (good is Electronics electronicsModel && electronicsModel.Model!.ToLower().Contains(findGoods.Model!.ToLower())))
                && (findGoods.Company == "" || (good is Electronics electronicsCompany && electronicsCompany.Company!.ToLower().Contains(findGoods.Company!.ToLower()))))
            {
                return true;
            }
            return false;
        }

    }

}