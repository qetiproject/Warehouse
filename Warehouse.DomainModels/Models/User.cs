using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Warehouse.DomainModels.Models
{
    public class User : IdentityUser<int>
    {
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
