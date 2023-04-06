using System.IO;
using System.Text;

namespace Warehouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            MyList allGoods = new MyList();
            allGoods.AddExistingGoods();
            allGoods.AddNewGoods();
            allGoods.ListOfAllGoods();
            
        }
    }
}

/*
static void Main(string[] args)
{
    bool isValid = false;
    int number = 0;
    int count = 0;

    while (!isValid)
    {
        Console.Write("Enter the number of dominoes: ");

        string? input = Console.ReadLine();
        if (input == null)
        {
            Message(ConsoleColor.Red, "\nInvalid input. Please enter an integer value.\n");
            continue;
        }

        try
        {
            number = int.Parse(input);
            isValid = true;
        }
        catch (FormatException)
        {
            Message(ConsoleColor.Red, "\nInvalid input. Please enter an integer value.\n");
        }
        catch (OverflowException)
        {
            Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
        }
    }*/