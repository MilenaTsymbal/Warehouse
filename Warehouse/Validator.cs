using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    internal class Validator
    {
        static public string GetTheValidationString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        return input;
                    }
                    else
                    {
                        Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                    }
                }
                catch (FormatException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. Please enter an string value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                }
            }
        }

        static public int GetTheValidationInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        return int.Parse(input);
                    }
                    else
                    {
                        Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                    }
                }
                catch (FormatException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. Please enter an string value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                }
            }
        }

        static public DateTime GetTheValidationTime(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        return DateTime.Parse(input);
                    }
                    else
                    {
                        Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                    }
                }
                catch (FormatException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. Please enter an string value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                }
            }
        }

        static void Message(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
