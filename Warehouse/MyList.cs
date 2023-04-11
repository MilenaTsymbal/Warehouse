using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace Warehouse
{
    public class MyList : List<Goods>
    {
        public MyList()
        {
            new FileWork().AddExistingGoods(this);
            Program.WorkingWithTheProgram(this);
        }

        

        public void AddNewGoods()
        {
            string nameOfGood = "";
            string unitOfMeasure = "";
            string unitPrice = "";
            string amount = "";
            DateTime dateOfLastDelivery = DateTime.Parse("01.01.0001 00:00:00");

            int lastItem = Count;

            int numberOfNewProducts = Validator.GetTheValidationInt("Enter the number of goods you want to add: ");
            Console.WriteLine();

            for (int i = 1; i <= numberOfNewProducts; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    switch (j)
                    {
                        case 0:
                            nameOfGood = Validator.GetTheValidationString("Write name of a good: ");
                            break;
                        case 1:
                            unitOfMeasure = Validator.GetTheValidationString("Write unit of measure of a good: ");
                            break;
                        case 2:
                            unitPrice = Validator.GetTheValidationString("Write unit of price of a good: ");
                            break;
                        case 3:
                            amount = Validator.GetTheValidationString("Write amount of delivered goods: ");
                            break;
                        case 4:
                            dateOfLastDelivery = Validator.GetTheValidationTime("Write date and time of last delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;

                    }
                }
                Goods product = new Goods(nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery);

                Add(product);
                Console.WriteLine();
            }
            Console.WriteLine();

            FileWork.AddNewGoodsToFile(this);

            Print.ProfitAndLossStatement(this, lastItem);
        }

        public void EditGood()
        {
            Print.ListOfAllGoods(this);

            int amountOfGoodsForChange = Validator.GetTheValidationInt("\nEnter the number of goods that will be changed: ");
            int indexOfGood = 0;

            for (int i = 0; i < amountOfGoodsForChange; i++)
            {
                indexOfGood = Validator.GetTheValidationForEditGoodInt("\nEnter the number of a good that you want to change: ");
                Console.WriteLine("\nTheae are all the characteristics that you can change:\n" +
                    "1. Name of a good\n2. Unit of measure\n3. Unit of price\n4. Amount\n5. Date of last delivery\n");
                List<int> characteristics = SortList(Validator.GetTheValidationCharacteristics("Enter the number / numbers of characteristic / characteristics that you want to change: \n"));

                foreach (int item in characteristics)
                {
                    switch (item - 1)
                    {
                        case 0:
                            this[indexOfGood - 1].NameOfGood = Validator.GetTheValidationString("\nWrite name of a good: ");
                            break;
                        case 1:
                            this[indexOfGood - 1].UnitOfMeasure = Validator.GetTheValidationString("\nWrite unit of measure of a good: ");
                            break;
                        case 2:
                            this[indexOfGood - 1].UnitPrice = Validator.GetTheValidationString("\nWrite unit of price of a good: ");
                            break;
                        case 3:
                            this[indexOfGood - 1].Amount = Validator.GetTheValidationString("\nWrite amount of delivered goods: ");
                            break;
                        case 4:
                            this[indexOfGood - 1].DateOfLastDelivery = Validator.GetTheValidationTime("\nWrite date and time of delivery of this good (in format dd.mm.yyyy hh:mm:ss): ");
                            break;
                    }
                }

            }

            FileWork.RewriteGoodsInFile(this);

            List<int> SortList(List<int> list)
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

        public void DeleteGoods()
        {
            Print.ListOfAllGoods(this);

            List<Goods> deletedGoods = new List<Goods>();

            int amountOfGoodsForChange = Validator.GetTheValidationInt("\nEnter the number of goods that will be deleted: ");
            int indexOfGood = 0;

            for (int i = 0; i < amountOfGoodsForChange; i++)
            {
                indexOfGood = Validator.GetTheValidationForEditGoodInt("\nEnter the number of a good that you want to delete: ");
                deletedGoods.Add(this[indexOfGood - 1]);
                RemoveAt(indexOfGood - 1);
            }

            Print.ListOfDeletedGoods(deletedGoods);
            FileWork.RewriteGoodsInFile(this);
        }

    }

}

