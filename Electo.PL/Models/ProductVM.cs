using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class ProductVM
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

        public string BrandName { get; set; }
        public string CategoryName { get; set; }

        public IFormFile PhotoUrl { get; set; }


    }
}
