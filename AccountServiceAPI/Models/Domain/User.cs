using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
	public class User : IdentityUser<Guid>
	{
		public Profile Profile { get; set; }
		public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
		public bool Enabled { get; set; }
		public bool Deleted { get; set; }
		public DateTime? LastEditedWhen { get; set; }
		public DateTime CreatedWhen { get; set; }
	}

	public class UserRole : IdentityUserRole<Guid>
	{
		public virtual User User { get; set; }
		public virtual Role Role { get; set; }
	}
	public class UserLogin : IdentityUserLogin<Guid> { }
	public class UserClaim : IdentityUserClaim<Guid> { }
	public class UserToken : IdentityUserToken<Guid> { }


}
