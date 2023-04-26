using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Warehouse
{
    internal class Validator
    {
       /* static public T GetTheValidationInput<T>(string message, Func<string, T> parser, Func<T, bool>? validator = null)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (input != null)
                {
                    try
                    {
                        T result = parser(input);

                        if (validator == null || validator(result))
                        {
                            return result;
                        }
                        else
                        {
                            Message(ConsoleColor.Red, $"\nInvalid input. The entered {typeof(T).Name.ToLower()} is not valid.\n");
                        }
                    }
                    catch (FormatException)
                    {
                        Message(ConsoleColor.Red, $"\nInvalid input. Please enter a valid {typeof(T).Name.ToLower()} value. Try to rewrite it.\n");
                    }
                    catch (OverflowException)
                    {
                        Message(ConsoleColor.Red, $"\nInvalid input. The entered {typeof(T).Name.ToLower()} value is too large or too small.\n");
                    }
                }
                else
                {
                    Message(ConsoleColor.Red, "\nInvalid input, nothing was written. Try to rewrite it.\n");
                }
            }
        }*/

        static public T GetTheValidationInput<T>(string message, Func<string, T> parser, Func<T, bool>? validator = null, bool allowNullInput = false)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (allowNullInput && input == null)
                {
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)"";
                    }
                }

                try
                {
                    T result = parser(input!);

                    if (validator == null || validator(result))
                    {
                        return result;
                    }
                    else
                    {
                        Print.Message(ConsoleColor.Red, $"\nInvalid input. The entered {typeof(T).Name.ToLower()} is not valid.\n");
                    }
                }
                catch (FormatException)
                {
                    Print.Message(ConsoleColor.Red, $"\nInvalid input. Please enter a valid {typeof(T).Name.ToLower()} value. Try to rewrite it.\n");
                }
                catch (OverflowException)
                {
                    Print.Message(ConsoleColor.Red, $"\nInvalid input. The entered {typeof(T).Name.ToLower()} value is too large or too small.\n");
                }
            }
        }


        static public List<int> GetTheValidationElements(string message)
        {
            return GetTheValidationInput(message, input =>
            {
                return input.Split(',').Select(int.Parse).ToList();
            }, elements =>
            {
                return !HasDuplicates(elements);
            });
        }

        static public int GetTheValidationAmountForDeletion(string message, Good good)
        {
            return GetTheValidationInput(message, int.Parse, userInput =>
            {
                return userInput >= 1 && userInput <= good.Amount;
            });
        }
        static public int GetTheValidationNumber(string message)
        {
            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Goods.txt";

            int lastLineNumber = File.ReadLines(filePath).Count();

            return GetTheValidationInput(message, int.Parse, userInput =>
            {
                return userInput >= 1 && userInput <= lastLineNumber;
            });
        }

        static public string GetTheValidationType(string message)
        {
            return GetTheValidationInput(message, input =>
            {
                return input;
            }, HasTheType);
        }

        static public string GetTheValidationSize(string message)
        {
            return GetTheValidationInput(message, input =>
            {
                return input;
            }, HasTheSize);
        }

        static public string GetTheValidationSizeNull(string message)
        {
            return GetTheValidationInput(message, input =>
            {
                return input;
            }, HasTheSize, allowNullInput: true);
        }

        private static bool HasDuplicates(List<int> characteristics)
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

        private static bool HasTheType(string input)
        {
            string type = input.ToLower();

            if (type == "food" || type == "drinks" || type == "clothing" || type == "footwear" || type == "electronics")
            {
                return true;
            }
            return false;
        }

        private static bool HasTheSize(string input)
        {
            string type = input.ToLower();

            foreach (char c in type)
            {
                if (c != 'x' && c != 'm' && c != 's' && c != 'l')
                {
                    return false;
                }
            }
            return true;
        }

       
    }
}
