using AccountService.Data;
using AccountService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Commands
{
	public class CreateRequestServiceCommandHandler : IHandleCommand<CreateRequestServiceCommand>
	{
		private readonly ProfileDbContext _profileDbContext;
		public CreateRequestServiceCommandHandler(ProfileDbContext profileDbContext)
		{
			_profileDbContext = profileDbContext;
		}
		public async Task<CommandHandlerResult> HandleAsync(CreateRequestServiceCommand command)
		{
			var model = command.Model;

			var profile = await _profileDbContext.Profiles.SingleOrDefaultAsync(q => q.ProfileId == model.RequesterId);

			if (profile == null)
			{
				return CommandHandlerResult.Error("Couldn't find the Profile");
			}

			var requestService = new RequestService
			{
				RequesterId = profile.ProfileId,
				Title = model.Title,
				Description = model.Description,
				ServiceTypeId = model.ServiceTypeId
			};

			await _profileDbContext.RequestServices.AddAsync(requestService);

			return CommandHandlerResult.OkDelayed(this, _ => new {
				RequesterId = requestService.RequesterId.ToString(),
				requestService.RequestServiceId,
				requestService.Title,
				requestService.Description,
				requestService.Completed,
				requestService.ServiceTypeId,
				ServiceTypeName = requestService.ServiceType?.Name ?? "",
				CreatedWhen = requestService.CreatedWhen.ToString()
			});
		}
	}
}
