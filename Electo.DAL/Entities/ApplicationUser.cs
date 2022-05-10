using Microsoft.AspNetCore.Identity;

namespace Electo.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
