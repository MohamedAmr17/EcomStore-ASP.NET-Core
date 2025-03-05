namespace Ecom.Models
{
   
        public class Product
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Image { get; set; }
            public decimal Price { get; set; }
    }
}