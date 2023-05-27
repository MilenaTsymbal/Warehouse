using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace Warehouse
{
    public class Warehouse : List<Good>
    {
        public List<Good> goods;

        public Warehouse()
        {
            goods = new List<Good>();
        }


        public void AddNewGoods(BaseOfInvoices incomeInvoices)
        {
            Invoice addedGoods = new Invoice();
            addedGoods.NumberOfInvoice = BaseOfInvoices.CountNumberOfInvoices(incomeInvoices);
            addedGoods.DateOfMakingInvoice = DateTime.Now;

            int lastItem = Count;

            int numberOfNewProducts = Validator.GetTheValidationInput("\n\nEnter the number of goods you want to add: ", int.Parse);

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
            Console.WriteLine("\nChoose the type of the product out of these:\n-Food\n-Clothing\n-Electronics");

            string category = Validator.GetTheValidationType("\nEnter the product type name: ");

            string nameOfGood = Validator.GetTheValidationGoodCharacteristic("Enter the good's name: ");

            string unitOfMeasure = Validator.GetTheValidationGoodCharacteristic("Enter unit of measure of a good: ");

            double unitPrice = Validator.GetTheValidationUnitPrice("Enter unit of price of a good: ");

            int amount = Validator.GetTheValidationAmount("Enter an amount of delivered goods: ");

            DateTime dateOfLastDelivery = Validator.GetTheValidationDateTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");

            switch (category)
            {
                case "food":
                case "drinks":
                    DateTime expiryDate = Validator.GetTheValidationInput("Enter an expiry date of a good (in the format dd.mm.yyyy):  ", DateTime.Parse);
                    return new Food(category, nameOfGood, unitOfMeasure, unitPrice, amount, expiryDate, dateOfLastDelivery);
                case "clothing":
                case "footwear":
                    string size = Validator.GetTheValidationSize("Enter the size of the good: ");
                    string color = Validator.GetTheValidationGoodCharacteristic("Enter the color of the good: ");
                    string brand = Validator.GetTheValidationGoodCharacteristic("Enter the good's brand name: ");
                    return new Clothing(category, nameOfGood, size, color, brand, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);
                case "electronics":
                    string model = Validator.GetTheValidationModel("Enter the model of the good: ");
                    string company = Validator.GetTheValidationGoodCharacteristic("Enter the name of the company that produces these goods: ");
                    return new Electronics(category, nameOfGood, model, company, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);
                default:
                    throw new InvalidOperationException("Invalid category: " + category);
            }
        }



        public void EditGood()
        {
            Print.PrintGoods<Good>(this, "List of all goods");

            Warehouse editedGoods = new Warehouse();

            int amountOfGoodsForChange = Validator.GetTheValidationNumberOfGoods("\n\nEnter the number of goods that will be changed: ", this);

            if (amountOfGoodsForChange > 0)
            {
                for (int i = 0; i < amountOfGoodsForChange; i++)
                {
                    int indexOfGood = Validator.GetTheValidationNumberOfGoods($"\nEnter the number of a good ({i + 1}/{amountOfGoodsForChange}) that you want to change: ", this);

                    EditGoodCharacteristics(indexOfGood);
                    editedGoods.Add(this[indexOfGood - 1]);
                }

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
            if (this[indexOfGood - 1] is Food food)
            {
                Food.EditFoodCharacteristics(food);
            }
            else if (this[indexOfGood - 1] is Clothing clothing)
            {
                Clothing.EditClothingCharacteristics(clothing);
            }
            else if (this[indexOfGood - 1] is Electronics electronics)
            {
                Electronics.EditElectronicsCharacteristics(electronics);
            }
        }



        public void DeleteGoods(BaseOfInvoices expenceInvoices)
        {
            Print.PrintGoods<Good>(this, "List of all goods");

            Invoice deletedGoods = new Invoice();
            deletedGoods.NumberOfInvoice = BaseOfInvoices.CountNumberOfInvoices(expenceInvoices);
            deletedGoods.DateOfMakingInvoice = DateTime.Now;

            int amountOfGoodsForDeletion = Validator.GetTheValidationNumberOfGoods("\nEnter the number of goods that will be deleted: ", this);

            if (amountOfGoodsForDeletion > 0)
            {
                for (int i = 0; i < amountOfGoodsForDeletion; i++)
                {
                    int indexOfGood = Validator.GetTheValidationNumberOfGoods($"\n\nEnter the number of a good ({i + 1}/" +
                        $"{amountOfGoodsForDeletion}) that will be dispatched: ", this);
                    int amountForDeletion = Validator.GetTheValidationAmountForDeletion($"\nEnter" +
                        $" the number of amount of a good ({i + 1}/{amountOfGoodsForDeletion}) " +
                        $"that will be dispatched: ", this[indexOfGood - 1]);

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