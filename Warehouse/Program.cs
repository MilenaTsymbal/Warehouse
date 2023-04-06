using System.IO;
using System.Text;

namespace Warehouse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

            MyList? list = new MyList();
            List<Goods> allGoods = new List<Goods>();
            list.AddExistingGoods(allGoods);
            list.AddNewGoods(allGoods);
            Console.WriteLine(  );
            list.ProfitAndLossStatement1(allGoods);
        }
    }
}

/*static void Message(ConsoleColor color, string message)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}

static int GetTheValidation(string side, int i)
{
    while (true)
    {
        Console.Write($"Enter the {side} number of {i + 1} domino: ");
        string? inputNumber = Console.ReadLine();

        try
        {
            if (inputNumber != null)
            {
                return int.Parse(inputNumber);
            }
            else
            {
                Message(ConsoleColor.Red, "\nInvalid input, did not write anything. Try to rewrite it.\n");
            }
        }
        catch (FormatException)
        {
            Message(ConsoleColor.Red, "\nInvalid input. Please enter an integer value. Try to rewrite it.\n");
        }
        catch (OverflowException)
        {
            Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
        }
    }

}
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