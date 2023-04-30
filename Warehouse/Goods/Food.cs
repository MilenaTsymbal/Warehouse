namespace Warehouse
{
    public class Food : Good
    {
        public DateTime ExpiryDate { get; set; }

        public Food(string category, string nameOfGood, string unitOfMeasure, int unitPrice, int amount, DateTime expiryDate, DateTime dateOfLastDelivery)
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            ExpiryDate = expiryDate;
        }
        public Food(Food other) : base(other)
        {
            ExpiryDate = other.ExpiryDate;
        }
    }
}
