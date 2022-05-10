using Electo.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Product { get; set; }


    }
}
