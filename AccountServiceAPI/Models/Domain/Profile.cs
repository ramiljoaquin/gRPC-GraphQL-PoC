using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
	public class Profile : BaseDomain
	{
		public Guid ProfileId { get; set; }
		public User User { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string CompanyName { get; set; }
		public string Website { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string PhotoThumbUrl { get; set; }
		public DateTime? BirthDate { get; set; }
		public string FullName
		{
			get
			{
				return $"{FirstName} {LastName}";
			}
		}
	}
}
