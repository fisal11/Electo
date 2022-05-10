using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
