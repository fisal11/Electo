using System.ComponentModel.DataAnnotations;

namespace Electo.Web.Models
{
	public class Contectus
	{
        public int Id { get; set; } 
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

    }
}
