using System.Drawing;
using System.Reflection;

namespace Warehouse
{
    public class Warehouse : List<Good>
    {
        public List<Good?> goods;

        public Warehouse()
        {
            goods = new List<Good?>();
        }

        public void AddNewGoods(Invoice allIncomeInvoice)
        {
            Warehouse addedGoods = new Warehouse();
            int lastItem = Count;

            int numberOfNewProducts = Validator.GetTheValidationInput("\nEnter the number of goods you want to add: ", int.Parse);

            for (int i = 1; i <= numberOfNewProducts; i++)
            {
                Good product = CreateNewGood();
                Add(product);
                addedGoods.Add(product);
                Console.WriteLine();
            }
            Console.WriteLine();

            FileWork.AddNewGoodsToFile(this);
            Invoice incomeInvoice = new Invoice
            {
                addedGoods
            };
            allIncomeInvoice.Add(addedGoods);
            Print.IncomeInvoice(incomeInvoice);
        }

        private Good CreateNewGood()
        {
            Console.WriteLine("\nChoose the type of product out of these:\n-Food\n-Drinks\n-Clothing\n-Shoes\n-Electronics");
            string category = Validator.GetTheValidationType("\nEnter the name of the type of the product: ");

            string nameOfGood = Validator.GetTheValidationInput("\nEnter the name of the good: ", s => s);

            string unitOfMeasure = Validator.GetTheValidationInput("Write unit of measure of a good: ", s => s);

            int unitPrice = Validator.GetTheValidationInput("Write unit of price of a good: ", int.Parse);

            int amount = Validator.GetTheValidationInput("Write amount of delivered goods: ", int.Parse);

            string dateOfLastDelivery = Validator.GetTheValidationInput("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ", s => s);

            switch (category)
            {
                case "food":
                case "drinks":
                    string expiryDate = Validator.GetTheValidationInput("Enter an expiry date of a product (in the format yyyy-MM-dd):  ", s => s);
                    return new Food(category, nameOfGood, unitOfMeasure, unitPrice, amount, expiryDate, dateOfLastDelivery);
                case "clothing":
                case "footwear":
                    string size = Validator.GetTheValidationSize("Enter the size of the product: ");
                    string color = Validator.GetTheValidationInput("Enter the color of the product: ", s => s);
                    string brand = Validator.GetTheValidationInput("Enter the name of the brand of the product: ", s => s);
                    return new Clothing(category, nameOfGood, size, color, brand, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);
                case "electronics":
                    string model = Validator.GetTheValidationInput("Enter the name of the model of the product: ", s => s);
                    string company = Validator.GetTheValidationInput("Enter the name of the company that produces this product: "
                        , s => s);
                    return new Electronics(category, nameOfGood, model, company, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);
                default:
                    throw new InvalidOperationException("Invalid category: " + category);
            }
        }


        public void EditGood()
        {
            Print.ListOfAllGoods(this);
            Warehouse editedGoods = new Warehouse();

            int amountOfGoodsForChange = Validator.GetTheValidationNumber("\nEnter the number of goods that will be changed: ");

            for (int i = 0; i < amountOfGoodsForChange; i++)
            {
                int indexOfGood = Validator.GetTheValidationNumber($"\nEnter the number of a good ({i + 1}/{amountOfGoodsForChange}) that you want to change: ");

                EditGoodCharacteristics(indexOfGood);
                editedGoods.Add(this[indexOfGood - 1]);
            }

            FileWork.RewriteGoodsInFile(this);
            Print.ListAfetrEditing(editedGoods);
        }

        private void EditGoodCharacteristics(int indexOfGood)
        {
            List<int> characteristics = new List<int>();

            if (this[indexOfGood - 1] is Food food)
            {
                Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
                "1. Name of a good\n2. Unit of measure\n3. Unit of price\n4. Amount\n5. Expiry date\n6. Date of last delivery\n");
                characteristics = SortList(Validator.GetTheValidationElements("Enter the number / numbers of characteristic / characteristics that you want to change: \n"));

                foreach (int item in characteristics)
                {
                    switch (item)
                    {
                        case 1:
                            food.NameOfGood = Validator.GetTheValidationInput("\nEnter the name of the good: ", s => s);
                            break;
                        case 2:
                            food.UnitOfMeasure = Validator.GetTheValidationInput("Write unit of measure of a good: ", s => s);
                            break;
                        case 3:
                            food.UnitPrice = Validator.GetTheValidationInput("Write unit of price of a good: ", int.Parse);
                            break;
                        case 4:
                            food.Amount = Validator.GetTheValidationInput("Write amount of delivered goods: ", int.Parse);
                            break;
                        case 5:
                            food.ExpiryDate = Validator.GetTheValidationInput("Write expiry date of this good (in format dd.mm.yyyy): ", s => s);
                            break;
                        case 6:
                            food.DateOfLastDelivery = Validator.GetTheValidationInput("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ", s => s);
                            break;
                    }
                }
            }
            else if (this[indexOfGood - 1] is Clothing clothing)
            {
                Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
       "1. Name of a good\n2. Size\n3. Brand\n4. Unit of measure\n5. Unit of price\n6. Amount\n7. Date of last delivery\n");
                characteristics = SortList(Validator.GetTheValidationElements("Enter the number / numbers of characteristic / characteristics that you want to change: \n"));

                foreach (int item in characteristics)
                {
                    switch (item)
                    {
                        case 1:
                            clothing.NameOfGood = Validator.GetTheValidationInput("\nEnter the name of the good: ", s => s);
                            break;
                        case 2:
                            clothing.Size = Validator.GetTheValidationSize("Enter the size of the product: ");
                            break;
                        case 3:
                            clothing.Brand = Validator.GetTheValidationInput("Enter the name of the brand of the product: ", s => s);
                            break;
                        case 4:
                            clothing.UnitOfMeasure = Validator.GetTheValidationInput("Write unit of measure of a good: ", s => s);
                            break;
                        case 5:
                            clothing.UnitPrice = Validator.GetTheValidationInput("Write unit of price of a good: ", int.Parse);
                            break;
                        case 6:
                            clothing.Amount = Validator.GetTheValidationInput("Write amount of delivered goods: ", int.Parse);
                            break;
                        case 7:
                            clothing.DateOfLastDelivery = Validator.GetTheValidationInput("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ", s => s);
                            break;
                    }
                }
            }
            else if (this[indexOfGood - 1] is Electronics electronics)
            {
                Console.WriteLine("\nThere are all the characteristics that you can change:\n" +
        "1. Name of a good\n2. Model\n3. Company\n4. Unit of measure\n5. Unit of price\n6. Amount\n7. Date of last delivery\n");
                characteristics = SortList(Validator.GetTheValidationElements("Enter the number / numbers of characteristic / characteristics that you want to change: \n"));

                foreach (int item in characteristics)
                {
                    switch (item)
                    {
                        case 1:
                            electronics.NameOfGood = Validator.GetTheValidationInput("\nEnter the name of the good: ", s => s);
                            break;
                        case 2:
                            electronics.Model = Validator.GetTheValidationInput("Enter the name of the model of the product: ", s => s);
                            break;
                        case 3:
                            electronics.Company = Validator.GetTheValidationInput("Enter the name of the company that produces this product: ", s => s);
                            break;
                        case 4:
                            electronics.UnitOfMeasure = Validator.GetTheValidationInput("Write unit of measure of a good: ", s => s);
                            break;
                        case 5:
                            electronics.UnitPrice = Validator.GetTheValidationInput("Write unit of price of a good: ", int.Parse);
                            break;
                        case 6:
                            electronics.Amount = Validator.GetTheValidationInput("Write amount of delivered goods: ", int.Parse);
                            break;
                        case 7:
                            electronics.DateOfLastDelivery = Validator.GetTheValidationInput("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ", s => s);
                            break;
                    }
                }
            }
        }


        public void DeleteGoods(Invoice allExpenceInvoice)
        {
            Print.ListOfAllGoods(this);
            Warehouse deletedGoods = new Warehouse();

            int amountOfGoodsForDeletion = Validator.GetTheValidationNumber("\nEnter numbers of goods that will be deleted: ");

            for (int i = 0; i < amountOfGoodsForDeletion; i++)
            {
                int indexOfGood = Validator.GetTheValidationNumber($"\nEnter the number of a good ({i + 1}/{amountOfGoodsForDeletion}) that you want to change: ");
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

            Invoice expenceInvoice = new Invoice
            {
                deletedGoods
            };
            allExpenceInvoice.Add(deletedGoods);
            Print.ExpenceInvoice(expenceInvoice);
            FileWork.RewriteGoodsInFile(this);
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
            FindGoods findGoods = CharacteristicsForFindingGoods();

            foreach (Good good in this)
            {
                if (IsGoodMatchingCriteria(good, findGoods))
                {
                    foundGoods.Add(good);
                }
            }
            Print.ListOfFindedGoods(foundGoods);
        }

        private FindGoods CharacteristicsForFindingGoods()
        {
            Console.WriteLine("\nChoose the type of product out of these:\n-Food\n-Drinks\n-Clothing\n-Shoes\n-Electronics");

            string category = Validator.GetTheValidationTypeForFinding("\nEnter the name of the type of the product: ");
            string nameOfGood = Validator.GetTheValidationInput("\nEnter the name of the good: ", s => s, allowNullInput: true);
            string unitOfMeasure = Validator.GetTheValidationInput("Write unit of measure of a good: ", s => s, allowNullInput: true);
            int unitPriceFrom = Validator.GetTheValidationInput("Write unit of price of a good(from): ", int.Parse, allowNullInput: true);
            int unitPriceTo = Validator.GetTheValidationInput("Write unit of price of a good(to): ", int.Parse, allowNullInput: true);
            int amountFrom = Validator.GetTheValidationInput("Write amount of delivered goods(from): ", int.Parse, allowNullInput: true);
            int amountTo = Validator.GetTheValidationInput("Write amount of delivered goods(to): ", int.Parse, allowNullInput: true);
            DateTime dateOfLastDeliveryFrom = Validator.GetTheValidationInput("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss)(from): ", DateTime.Parse, allowNullInput: true);
            DateTime dateOfLastDeliveryTo = Validator.GetTheValidationInput("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss)(to): ", DateTime.Parse, allowNullInput: true);
            DateTime expiryDateFrom = Validator.GetTheValidationInput("Enter an expiry date of a product (in the format dd.mm.yyyy)(from):  ", DateTime.Parse, allowNullInput: true);
            DateTime expiryDateTo = Validator.GetTheValidationInput("Enter an expiry date of a product (in the format dd.mm.yyyy)(to):  ", DateTime.Parse, allowNullInput: true);
            string size = Validator.GetTheValidationSizeNull("Enter the size of the product: ");
            string color = Validator.GetTheValidationInput("Enter the color of the product: ", s => s, allowNullInput: true);
            string brand = Validator.GetTheValidationInput("Enter the name of the brand of the product: ", s => s, allowNullInput: true);
            string model = Validator.GetTheValidationInput("Enter the name of the model of the product: ", s => s, allowNullInput: true);
            string company = Validator.GetTheValidationInput("Enter the name of the company that produces this product: "
                , s => s, allowNullInput: true);

            return new FindGoods(category, nameOfGood, unitOfMeasure, unitPriceFrom, unitPriceTo, amountFrom, amountTo, dateOfLastDeliveryFrom, dateOfLastDeliveryTo, expiryDateFrom, expiryDateTo, size, color, brand, model, company);

        }

        private bool IsGoodMatchingCriteria(Good good, FindGoods findGoods)
        {
            if ((findGoods.NameOfGood == "" || good.NameOfGood!.ToLower().Contains(findGoods.NameOfGood.ToLower()))
                && (findGoods.Category == "" || good.Category!.ToLower().Contains(findGoods.Category.ToLower()))
                && (findGoods.UnitOfMeasure == "" || good.UnitOfMeasure!.ToLower().Contains(findGoods.UnitOfMeasure.ToLower()))
                && (findGoods.UnitPriceFrom == 0 || findGoods.UnitPriceFrom <= good.UnitPrice)
                && (findGoods.UnitPriceTo == 0 || findGoods.UnitPriceTo >= good.UnitPrice)
                && (findGoods.AmountFrom == 0 || findGoods.AmountFrom <= good.Amount)
                && (findGoods.AmountTo == 0 || findGoods.AmountTo >= good.Amount)
                && (findGoods.DateOfLastDeliveryFrom == DateTime.MinValue || findGoods.DateOfLastDeliveryFrom <= DateTime.Parse(good.DateOfLastDelivery))
                && (findGoods.DateOfLastDeliveryTo == DateTime.MinValue || findGoods.DateOfLastDeliveryTo >= DateTime.Parse(good.DateOfLastDelivery))
                && (findGoods.ExpiryDateFrom == DateTime.MinValue || (good is Food foodExpiryDateFrom && findGoods.ExpiryDateFrom <= DateTime.Parse(foodExpiryDateFrom.ExpiryDate)))
                && (findGoods.ExpiryDateTo == DateTime.MinValue || (good is Food foodExpiryDateTo && findGoods.ExpiryDateTo >= DateTime.Parse(foodExpiryDateTo.ExpiryDate)))
                && (findGoods.Size == "" || (good is Clothing clothingSize && clothingSize.Size!.ToLower().Contains(findGoods.Size.ToLower())))
                && (findGoods.Color == "" || (good is Clothing clothingColor && clothingColor.Color!.ToLower().Contains(findGoods.Color.ToLower())))
                && (findGoods.Brand == "" || (good is Clothing clothingBrand && clothingBrand.Brand!.ToLower().Contains(findGoods.Brand.ToLower())))
                && (findGoods.Model == "" || (good is Electronics electronicsModel && electronicsModel.Model!.ToLower().Contains(findGoods.Model.ToLower())))
                && (findGoods.Company == "" || (good is Electronics electronicsCompany && electronicsCompany.Company!.ToLower().Contains(findGoods.Company.ToLower()))))
            {
                return true;
            }
            return false;
        }
        

        private List<int> SortList(List<int> list)
        {
            int temp = 0;

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - 1 - i; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        temp = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

    }

}