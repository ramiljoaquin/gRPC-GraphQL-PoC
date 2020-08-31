using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
	public class RequestService
	{
		public int RequestServiceId { get; set; }
		public Guid RequesterId { get; set; }
		public Profile Requester { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int? ServiceTypeId { get; set; }
		public ServiceType ServiceType { get; set; } /* Service Type */
		public DateTime CreatedWhen { get; set; }
		public bool Completed { get; set; }
		public DateTime CompletedWhen { get; set; }
		public ICollection<RequestServiceStatus> ServiceStatuses { get; set; }
	}

	public class RequestServiceStatus : BaseDomain
	{
		public int RequestServiceId { get; set; }
		public int? ServiceStatusId { get; set; }
		public ServiceStatus ServiceStatus { get; set; }
		public string Description { get; set; }
	}

	public class ServiceStatus
	{
		public int ServiceStatusId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Deleted { get; set; }
	}

	public class ServiceType
	{
		public int ServiceTypeId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Deleted { get; set; }
	}
	
}
