namespace Warehouse
{
    public class Food : Good
    {
        public string ExpiryDate { get; set; }

        public Food(string category, string nameOfGood, string unitOfMeasure, string unitPrice, int amount, string expiryDate, string dateOfLastDelivery)
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
