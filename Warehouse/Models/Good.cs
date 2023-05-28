namespace Warehouse
{
    public class Good
    {
        public string Category { get; set; }
        public string NameOfGood { get; set; }
        public string UnitOfMeasure { get; set; }
        public double UnitPrice { get; set; }
        public int Amount { get; set; }
        public DateTime DateOfLastDelivery { get; set; }

        public Good(string category, string nameOfGood, string unitOfMeasure, double unitPrice, int amount, DateTime dateOfLastDelivery)
        {
            Category = category;
            NameOfGood = nameOfGood;
            UnitOfMeasure = unitOfMeasure;
            UnitPrice = unitPrice;
            Amount = amount;
            DateOfLastDelivery = dateOfLastDelivery;
        }
        public Good(Good other)
        {
            Category = other.Category;
            NameOfGood = other.NameOfGood;
            UnitOfMeasure = other.UnitOfMeasure;
            UnitPrice = other.UnitPrice;
            Amount = other.Amount;
            DateOfLastDelivery = other.DateOfLastDelivery;
        }
    }
}
