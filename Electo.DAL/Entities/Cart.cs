using System.ComponentModel.DataAnnotations;

namespace Electo.DAL.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string CustomerId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }
    }

}
