using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Service.DataAccess.Entity
{
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// Create a new user object without private information
        /// </summary>
        /// <returns></returns>
        public User CreateUserWithoutPrivateInfo()
        {
            return new User()
            {
                Id = Id,
                UserName = UserName,
                Email = Email,
                EmailConfirmed = EmailConfirmed,
                PhoneNumber = PhoneNumber,
                PhoneNumberConfirmed = PhoneNumberConfirmed,
                Department = Department,
                ConcurrencyStamp = ConcurrencyStamp,
                SecurityStamp = SecurityStamp,
            };
        }
    }
}
