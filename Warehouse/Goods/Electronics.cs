namespace Warehouse
{
    internal class Electronics : Good
    {
        public string Model { get; set; }
        public string Company { get; set; }

        public Electronics(string category, string nameOfGood, string model, string company, string unitOfMeasure, double unitPrice, int amount, DateTime dateOfLastDelivery) 
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            Model = model;
            Company = company;
        }
        public Electronics(Electronics other) : base(other)
        {
            Model = other.Model;
            Company = other.Company;
        }
    }
}
