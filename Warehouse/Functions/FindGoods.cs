using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{ 
    public class FindGoods: Good
    {
        public new string? Category { get; set; }
        public new string? NameOfGood { get; set; }
        public new string? UnitOfMeasure { get; set; }
        public new string? UnitPrice { get; set; }
        public new int? Amount { get; set; }
        public new string? DateOfLastDelivery { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }
        public string? ExpiryDate { get; set; }


        /* public FindGoods(string category, string nameOfGood, string unitOfMeasure, string unitPrice, int amount, string dateOfLastDelivery, string expiryDate)
            : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
         {
             Category = category;
             NameOfGood = nameOfGood;
             UnitOfMeasure = unitOfMeasure;
             UnitPrice = unitPrice;
             Amount = amount;
             DateOfLastDelivery = dateOfLastDelivery;
             ExpiryDate = expiryDate;
         }

         public FindGoods(string category, string nameOfGood, string unitOfMeasure, string unitPrice, int amount, string dateOfLastDelivery, string size, string color, string brand)
           : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
         {
             Category = category;
             NameOfGood = nameOfGood;
             UnitOfMeasure = unitOfMeasure;
             UnitPrice = unitPrice;
             Amount = amount;
             DateOfLastDelivery = dateOfLastDelivery;
             Size = size;
             Color = color;
             Brand = brand;
         }

         public FindGoods(string category, string nameOfGood, string unitOfMeasure, string unitPrice, int amount, string dateOfLastDelivery, string model, string company)
         : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
         {
             Category = category;
             NameOfGood = nameOfGood;
             UnitOfMeasure = unitOfMeasure;
             UnitPrice = unitPrice;
             Amount = amount;
             DateOfLastDelivery = dateOfLastDelivery;
             Model = model;
             Company = company;
         }
 */
        public FindGoods(string category, string nameOfGood, string unitOfMeasure, string unitPrice, int amount, string dateOfLastDelivery, string expiryDate, string size, string color, string brand, string model, string company)
         : base(category, nameOfGood, unitOfMeasure, unitPrice, amount, dateOfLastDelivery)
        {
            Category = category;
            NameOfGood = nameOfGood;
            UnitOfMeasure = unitOfMeasure;
            UnitPrice = unitPrice;
            Amount = amount;
            DateOfLastDelivery = dateOfLastDelivery;
            ExpiryDate = expiryDate;
            Size = size;
            Color = color;
            Brand = brand;
            Model = model;
            Company = company;
        }
    }
}
