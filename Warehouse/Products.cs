using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public class Goods
    {
        public string NameOfGood { get; set; }
        public string UnitOfMeasure { get; set; }
        public string UnitPrice { get; set; }
        public string Amount { get; set; }
        public DateTime DateOfLastDelivery { get; set; }

        public Goods(string nameOfGood, string unitOfMeasure, string unitPrice, string amount, DateTime dateOfLastDelivery)
        {
            NameOfGood = nameOfGood;
            UnitOfMeasure = unitOfMeasure;
            UnitPrice = unitPrice;
            Amount = amount;
            DateOfLastDelivery = dateOfLastDelivery;
        }

    }
}
