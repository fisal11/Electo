using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class OrderVM
    {
        [Key]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Please Enter The FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter The LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter The Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Enter The The City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter Province")]
        public string Province { get; set; }
        [Required(ErrorMessage = "Please Enter The PostalCode")]
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Please Enter The PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<OrderDetailVM> OrderDetailsvm { get; set; }
    }
}
