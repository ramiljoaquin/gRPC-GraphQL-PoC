using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Commands
{
	public class UpdateProfileCommandHandler : IHandleCommand<UpdateProfileCommand>
	{
		private readonly ProfileDbContext _profileDbContext;
		public UpdateProfileCommandHandler(ProfileDbContext profileDbContext)
		{
			_profileDbContext = profileDbContext;
		}
		public async Task<CommandHandlerResult> HandleAsync(UpdateProfileCommand command)
		{
			var model = command.Model;

			var profile = await _profileDbContext.Profiles.SingleOrDefaultAsync(q => q.ProfileId == model.ProfileId);

			if (profile == null)
			{
				return CommandHandlerResult.Error("Couldn't find the Profile");
			}

			profile.FirstName = model.FirstName;
			profile.LastName = model.LastName;

			if (!string.IsNullOrEmpty(model.PhotoThumbUrl))
			{
				profile.PhotoThumbUrl = model.PhotoThumbUrl;
			}

			_profileDbContext.Profiles.Update(profile);

			return CommandHandlerResult.OkDelayed(this, _ => new { 
				ProfileId = profile.ProfileId.ToString(),
				profile.FirstName,
				profile.LastName,
				PhotoThumbUrl = profile.PhotoThumbUrl ?? ""
			});
		}
	}
}