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

        static public int GetTheValidationForEditGoodInt(string message)
        {
            while (true)
            {
                string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\File.txt";

                int lastLineNumber = File.ReadLines(filePath).Count();

                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        int userInput = int.Parse(input);

                        if (userInput >= 1 && userInput <= lastLineNumber)
                        {
                            return userInput;
                        }
                        else
                        {
                            Message(ConsoleColor.Red, "\nInvalid input. The entered value is too large or too small.\n");
                        }
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
        }//check if there is this number in the file of goods

        static public List<int> GetTheValidationCharacteristics(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                try
                {
                    if (input != null)
                    {
                        List<int> characteristics = input.Split(',').Select(int.Parse).ToList();

                        if (!HasDuplicates(characteristics))
                        {
                            return characteristics;
                        }
                        else
                        {
                            Message(ConsoleColor.Red, "\nInvalid input, the same characteristics were written. Try to rewrite them.\n");
                        }
                        
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

        public static bool HasDuplicates(List<int> characteristics)
        {
            HashSet<int> unique = new HashSet<int>();

            foreach (int item in characteristics)
            {
                if (!unique.Add(item))
                {
                    return true;
                }
            }
            return false;
        }

        static void Message(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
