using AccountService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Commands
{
	public class CreateRequestServiceCommand : CommandBase
	{
		public CreateRequestServiceCommand(CreateRequestServiceModel createRequestServiceModel)
		{
			Model = createRequestServiceModel;
		}

		protected override IEnumerable<string> OnValidation()
		{
			if (Model.RequesterId == Guid.Empty)
			{
				yield return $"{nameof(Model.RequesterId)} is required";
			}
			if (string.IsNullOrEmpty(Model.Title))
			{
				yield return $"{nameof(Model.Title)} is required";
			}
			if (string.IsNullOrEmpty(Model.Description))
			{
				yield return $"{nameof(Model.Description)} is required";
			}
		}

		public CreateRequestServiceModel Model { get; }
	}
}
