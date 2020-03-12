using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Models
{
    public class Stock
    {
       

        public int StockId { get; set; }
        public int StoreId { get; set; }
        public int ItemId { get; set; }

        public string StoreName { get; set; }
        public string ItemName { get; set; }

        public int PurchaseQuantity { get; set; }
        public int SaleReturnQuantity { get; set; }
        public int SaleQuantity { get; set; }
        public int TransferQuantity { get; set; }
        public int PurchaseReturnQuantity { get; set; }
        public string Invoice { get; set; }
        public decimal AverageRate { get; set; }
        public int AvailableQuantity { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Date { get; set; }

    }
}