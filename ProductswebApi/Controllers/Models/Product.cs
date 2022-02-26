using System;
namespace ProductWebApi.Models
{

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }


}