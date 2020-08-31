using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
	public class CreateRequestServiceModel
	{
		public Guid RequesterId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ServiceTypeId { get; set; }
	}
}
