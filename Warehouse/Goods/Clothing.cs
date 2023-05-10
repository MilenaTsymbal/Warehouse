using System.Drawing;

namespace Warehouse
{
    internal class Clothing : Good
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public Clothing(string category, string nameOfGood, string size, string color, string brand, string unitOfMeasure, double unitPrice, int amount, DateTime dateOfLastDelivery)
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            Size = size;
            Color = color;
            Brand = brand;
        }
        public Clothing(Clothing other) : base(other)
        {
            Size = other.Size;
            Color = other.Color;
            Brand = other.Brand;
        }
    }
}
