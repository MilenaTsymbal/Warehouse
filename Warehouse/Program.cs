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

            MyList allGoods = new MyList();
            allGoods.AddExistingGoods();
            //allGoods.AddNewGoods();
            allGoods.EditGoodInfo();
            allGoods.DeleteGoods();


        }
    }
}