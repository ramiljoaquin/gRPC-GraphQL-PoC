using AccountService.Commands;
using AccountService.Models;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Services
{
	public class AccountCommandService : AccountCommand.AccountCommandBase
	{
		private readonly ICommandBus _commandBus;
		public AccountCommandService(ICommandBus commandBus)
		{
			_commandBus = commandBus;
		}
		public override async Task<SignInResponse> SignIn(SignInRequest request, ServerCallContext context)
		{
			var signInModel = new SignInModel
			{
				UserName = request.UserName,
				Password = request.Password
			};

			var signInCommand = new SignInCommand(signInModel);
			var result = await _commandBus.SendAsync(signInCommand);

			if (result.IsOk)
			{

				var response = result.Value as dynamic;

				return new SignInResponse
				{
					AccessToken = response.AccessToken,
					RefreshToken = response.RefreshToken,
					ExpiresIn = response.ExpiresIn,
					TokenType = response.TokenType,
				};
			}

			var statusCode = (StatusCode)result.StatusCode;

			throw new RpcException(new Status(statusCode, result.Value?.ToString()));
		}

		public override async Task<SignUpResponse> SignUp(SignUpRequest request, ServerCallContext context)
		{
			var signUpModel = new SignUpModel
			{
				CompanyName = request.CompanyName,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Phone = request.Phone,
				Website = request.Website,
				Email = request.Email,
				Password = request.Password,
				RoleName = request.RoleName
			};
			var signUpCommand = new SignUpCommand(signUpModel);
			var result = await _commandBus.TransactionSendAsync(signUpCommand);
			if (result.IsOk)
			{
				var response = result.Value as dynamic;

				return new SignUpResponse
				{
					UserId = response.UserId,
					Link = response.Link
				};
			}

			var statusCode = (StatusCode)result.StatusCode;

			throw new RpcException(new Status(statusCode, result.Value?.ToString()));
		}
	}
}
