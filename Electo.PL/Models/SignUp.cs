using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electo.PL.Models
{
    public class SignUp
    {
            

        [Required(ErrorMessage = "Please Enter Your User Name")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Adress")]
        [DataType(DataType.Text)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Please Enter Your phone")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Please Enter ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Not Matching")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
