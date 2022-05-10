using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class CartVM
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public ProductVM Productvm { get; set; }
    }
}
