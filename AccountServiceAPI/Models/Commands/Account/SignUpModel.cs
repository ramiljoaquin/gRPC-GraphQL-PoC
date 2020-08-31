using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
	public class SignUpModel
	{
		public string Email { get; set; }
		public string CompanyName { get; set; }
		public string RoleName { get; set; } = "User";
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string Website { get; set; }
		public string Phone { get; set; }
	}
}
