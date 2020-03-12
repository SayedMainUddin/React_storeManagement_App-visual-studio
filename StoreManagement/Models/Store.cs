using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
    }
}