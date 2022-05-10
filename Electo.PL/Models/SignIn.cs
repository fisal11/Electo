using System.ComponentModel.DataAnnotations;

namespace Electo.PL.Models
{
    public class SignIn
    {

        [Required(ErrorMessage = "Please Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Rememberme { get; set; }

    }
}
