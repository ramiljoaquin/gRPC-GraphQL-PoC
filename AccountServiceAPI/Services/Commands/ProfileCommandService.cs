using AccountService.Commands;
using AccountService.Models;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Services
{
	public class ProfileCommandService : ProfileCommand.ProfileCommandBase
	{
		private readonly ICommandBus _commandBus;
		public ProfileCommandService(ICommandBus commandBus)
		{
			_commandBus = commandBus;
		}

		public override async Task<UpdateProfileResponse> UpdateProfile(UpdateProfileRequest request, ServerCallContext context)
		{
			var updateProfileModel = new UpdateProfileModel
			{
				ProfileId = Guid.Parse(request.ProfileId),
				FirstName = request.FirstName,
				LastName = request.LastName,
				PhotoThumbUrl = request.PhotoThumbUrl
			};

			var updateProfileCommand = new UpdateProfileCommand(updateProfileModel);

			var result = await _commandBus.TransactionSendAsync(updateProfileCommand);
			if (result.IsOk)
			{
				var response = result.Value as dynamic;

				return new UpdateProfileResponse
				{
					ProfileId = response.ProfileId,
					FirstName = response.FirstName,
					LastName = response.LastName,
					PhotoThumbUrl = response.PhotoThumbUrl,
				};
			}

			var statusCode = (StatusCode)result.StatusCode;

			throw new RpcException(new Status(statusCode, result.Value?.ToString()));
		}
	}
}

