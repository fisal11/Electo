namespace Electo.PL.Models
{
    public class OrderDetailVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public int OrderId { get; set; }
        public OrderVM Ordervm { get; set; }
    }
}
