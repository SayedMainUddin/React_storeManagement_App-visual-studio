using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Models
{
    public class Item
    {

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
  

    }
}