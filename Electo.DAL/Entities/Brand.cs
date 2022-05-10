using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.DAL.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        public string BrandName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
