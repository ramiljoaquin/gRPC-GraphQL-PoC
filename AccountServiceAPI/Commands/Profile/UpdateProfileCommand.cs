using AccountService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Commands
{
	public class UpdateProfileCommand : CommandBase
	{
		public UpdateProfileCommand(UpdateProfileModel updateProfileModel)
		{
			Model = updateProfileModel;
		}

		protected override IEnumerable<string> OnValidation()
		{
			if (Model.ProfileId == Guid.Empty)
			{
				yield return $"{nameof(Model.ProfileId)} is required";
			}

			if (string.IsNullOrEmpty(Model.FirstName))
			{
				yield return $"{nameof(Model.FirstName)} is required";
			}
			if (string.IsNullOrEmpty(Model.LastName))
			{
				yield return $"{nameof(Model.LastName)} is required";
			}
		}

		public UpdateProfileModel Model { get; }
	}
}
