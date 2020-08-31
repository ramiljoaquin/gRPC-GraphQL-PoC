using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role() { }
        public Role(string roleName)
        {
            Name = roleName;
        }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }

    public class RoleClaim : IdentityRoleClaim<Guid> { }
}
