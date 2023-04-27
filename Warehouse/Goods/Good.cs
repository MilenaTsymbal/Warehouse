namespace Warehouse
{
    public class Good
    {
        public string Category { get; set; }
        public string NameOfGood { get; set; }
        public string UnitOfMeasure { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
        public string DateOfLastDelivery { get; set; }

        public Good(string category, string nameOfGood, string unitOfMeasure, int unitPrice, int amount, string dateOfLastDelivery)
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
