using System.IO;
using System.Text;

namespace Warehouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Warehouse goods = new Warehouse();
            Invoice allIncomeInvoice = new Invoice();
            Invoice allExpenceInvoice = new Invoice();

            FileWork.AddExistingGoods(goods);
            WorkingWithTheProgram(goods, allIncomeInvoice, allExpenceInvoice);

        }

        public static void WorkingWithTheProgram(Warehouse goods, Invoice allIncomeInvoice, Invoice allExpenceInvoice)
        {
           /* goods.FindGood();*/

            while (true)
            {
                Print.Message(ConsoleColor.Yellow, "\nChoose the command out of these: \n\n1.Add new goods\n\n2.Edit Goods\n\n3.Delete goods" +
                    "\n\n4.Show the list of all goods\n\n5.Find a good by characteristics\n\n6.Show all the income invoices" +
                    "\n\n7.Show all the expence invoices\n");
                Console.Write("\nEnter the command: ");
                string? command = Console.ReadLine();
               
                if (command != null)
                {
                    switch (command)
                    {
                        case "1":
                            goods.AddNewGoods(allIncomeInvoice);
                            break;
                        case "2":
                            goods.EditGood();
                            break;
                        case "3":
                            goods.DeleteGoods(allExpenceInvoice);
                            break;
                        case "4":
                            /*Print.ListOfAllGoods(goods);*/
                            Print.WayOfPrinting(goods);
                            break;
                        case "5":
                            goods.FindGoods();
                            break;
                        case "6":
                            Print.IncomeInvoice(allIncomeInvoice);
                            break;
                        case "7":
                            Print.ExpenceInvoice(allExpenceInvoice);
                            break;
                        case "exit":
                            return;
                        default:
                            Console.WriteLine("\nValidator.Invalid command!");
                            break;
                    }
                }

            }
        }
    }
}