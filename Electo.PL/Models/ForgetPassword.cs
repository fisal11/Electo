using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class ForgetPassword
    {

        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
