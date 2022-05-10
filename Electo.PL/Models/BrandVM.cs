using Electo.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class BrandVM
    {
        public int Id { get; set; }
        [Required]
        public string BrandName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
