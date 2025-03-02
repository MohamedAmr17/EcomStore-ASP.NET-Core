using Microsoft.AspNetCore.Identity;

namespace Ecom.Data
{

    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; } = string.Empty;
    }
}
