using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Warehouse
{
    internal class Validator
    {
        static public T GetTheValidationInput<T>(string message, Func<string, T> parser, Func<T, bool>? validator = null, bool allowNullInput = false)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (allowNullInput && string.IsNullOrWhiteSpace(input))
                {
                    return GetDefaultValue<T>();
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

        static T GetDefaultValue<T>()
        {
            if (typeof(T) == typeof(string))
            {
                return (T)(object)"";
            }
            else if (typeof(T) == typeof(int))
            {
                return (T)(object)0;
            }
            else if (typeof(T) == typeof(DateTime))
            {
                return (T)(object)DateTime.MinValue;
            }
            else
            {
                return default!;
            }
        }

        //for number that exsists in the range of excisting goods
        static public int GetTheValidationNumberOfGoods(string message)
        {
            string filePath = @"C:\Users\Админ\source\repos\Warehouse\Warehouse\Goods\Goods.txt";

            int lastLineNumber = File.ReadLines(filePath).Count();

            return GetTheValidationInput(message, int.Parse, userInput =>
            {
                return userInput >= 1 && userInput <= lastLineNumber;
            });
        }


        //for string inputs of characteristics
        static public string GetTheValidationGoodCharacteristic(string message, bool allowNullInput = false)
        {
            return GetTheValidationInput(message, s => s.ToLower(), userInput =>
            {
                return !string.IsNullOrEmpty(userInput);
            }, allowNullInput);
        }

        static public string GetTheValidationType(string message, bool allowNullInput = false)
        {
            return GetTheValidationInput(message, input =>
            {
                return input;
            }, HasTheType, allowNullInput);
        }

        static public double GetTheValidationUnitPrice(string message, bool allowNullInput = false)
        {
            return GetTheValidationInput(message, double.Parse, userInput =>
            {
                return userInput > 0 && userInput == Math.Round(userInput, 2);
            }, allowNullInput);
        }

        static public int GetTheValidationAmount(string message, bool allowNullInput = false)
        {
            return GetTheValidationInput(message, int.Parse, userInput =>
            {
                return userInput > 0;
            }, allowNullInput);
        }
       
        static public DateTime GetTheValidationDateTime(string message, bool allowNullInput = false)
        {
            return GetTheValidationInput(message, DateTime.Parse, input =>
            {
                DateTime dateNow = DateTime.Now;

                return input <= dateNow;
            }, allowNullInput);
        }

        static public string GetTheValidationSize(string message, bool allowNullInput = false)
        {
            return GetTheValidationInput(message, s => s, HasTheSize, allowNullInput);
        }


        static public int GetTheValidationAmountForDeletion(string message, Good good)
        {
            return GetTheValidationInput(message, int.Parse, userInput =>
            {
                return userInput >= 1 && userInput <= good.Amount;
            });
        }

        static public List<int> GetTheValidationListOfCharacteristics(string message)
        {
            return GetTheValidationInput(message, input =>
            {
                return input.Split(',').Select(int.Parse).ToList();
            }, elements =>
            {
                return !HasDuplicates(elements);
            });
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

            if (type == "food" || type == "clothing" || type == "electronics")
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
