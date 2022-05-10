using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Descrption { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int CatId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
