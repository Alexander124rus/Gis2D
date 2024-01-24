
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GeoMVC7.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public static explicit operator ApplicationUser(Task<ApplicationUser?> v)
        {
            throw new NotImplementedException();
        }

        
    }
}
