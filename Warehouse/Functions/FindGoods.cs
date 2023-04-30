using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{ 
    public class FindGoods
    {
        public string? Category { get; set; }
        public string? NameOfGood { get; set; }
        public string? UnitOfMeasure { get; set; }
        public int UnitPriceFrom { get; set; }
        public int UnitPriceTo { get; set; }
        public int AmountFrom { get; set; }
        public int AmountTo { get; set; }
        public DateTime DateOfLastDeliveryFrom { get; set; }
        public DateTime DateOfLastDeliveryTo { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }
        public DateTime ExpiryDateFrom { get; set; }
        public DateTime ExpiryDateTo { get; set; }

      /*  public FindGoods(string category, string nameOfGood, string unitOfMeasure, int unitPriceFrom, int unitPriceTo, 
            int amountFrom, int amountTo, DateTime dateOfLastDeliveryFrom, DateTime dateOfLastDeliveryTo, DateTime expiryDateFrom, 
            DateTime expiryDateTo, string size, string color, string brand, string model, string company)
        {
            Category = category;
            NameOfGood = nameOfGood;
            UnitOfMeasure = unitOfMeasure;
            UnitPriceFrom = unitPriceFrom;
            UnitPriceTo = unitPriceTo;
            AmountFrom = amountFrom;
            AmountTo = amountTo;
            DateOfLastDeliveryFrom = dateOfLastDeliveryFrom;
            DateOfLastDeliveryTo = dateOfLastDeliveryTo;
            ExpiryDateFrom = expiryDateFrom;
            ExpiryDateTo = expiryDateTo;
            Size = size;
            Color = color;
            Brand = brand;
            Model = model;
            Company = company;
        }*/
    }
}
