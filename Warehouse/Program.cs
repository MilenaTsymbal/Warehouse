using System.IO;
using System.Text;

namespace Warehouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseOfInvoices incomeInvoices = new BaseOfInvoices();
            FileWork.AddExistingIncomeInvoices(incomeInvoices);

            BaseOfInvoices expenceInvoices = new BaseOfInvoices();
            FileWork.AddExistingExpenceInvoices(expenceInvoices);

            Warehouse goods = new Warehouse();
            FileWork.AddExistingGoods(goods);
            WorkingWithTheProgram(goods, incomeInvoices, expenceInvoices);

        }

        public static void WorkingWithTheProgram(Warehouse goods, BaseOfInvoices incomeInvoices, BaseOfInvoices expenceInvoices)
        {
            while (true)
            {
                Print.Message(ConsoleColor.Yellow, "\nChoose the command out of these: \n\n1.Add new goods to the warehouse" +
                    "\n\n2.Edit goods of the warehouse\n\n3.Delete goods from the warehouse\n\n4.Show the list of all goods of the warehouse" +
                    "\n\n5.Find a good by characteristics\n\n6.Show all the income invoices\n\n7.Show all the expence invoices\n");
                Console.Write("\nEnter the command (if you want to exit, write \"exit\"): ");
                string? command = Console.ReadLine();
               
                if (command != null)
                {
                    switch (command)
                    {
                        case "1":
                            goods.AddNewGoods(incomeInvoices);
                            break;
                        case "2":
                            goods.EditGood();
                            break;
                        case "3":
                            goods.DeleteGoods(expenceInvoices);
                            break;
                        case "4":
                            Print.ListOfAllGoods(goods);
                            break;
                        case "5":
                            goods.FindGoods();
                            break;
                        case "6":
                            Print.IncomeInvoices(incomeInvoices);
                            break;
                        case "7":
                            Print.ExpenceInvoices(expenceInvoices);
                            break;
                        case "exit":
                            return;
                        default:
                            Print.Message(ConsoleColor.Red, "\n\nInvalid command! Try to rewrite a command.\n");
                            break;
                    }
                }

            }
        }
    }
}