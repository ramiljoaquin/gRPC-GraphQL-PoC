using Microsoft.AspNetCore.Identity;
using AccountService.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using AccountService.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace AccountService.Commands
{
	public class SignInCommandHandler : IHandleCommand<SignInCommand>
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly ProfileDbContext _profileDbContext;
		private readonly ITokenProvider _tokenProvider;
		public SignInCommandHandler(
		  UserManager<User> userManager,
		  SignInManager<User> signInManager,
		  ProfileDbContext profileDbContext,
		  ITokenProvider tokenProvider)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_profileDbContext = profileDbContext;
			_tokenProvider = tokenProvider;
		}

		public async Task<CommandHandlerResult> HandleAsync(SignInCommand command)
		{

			var model = command.Model;

			var user = await _profileDbContext.Users.Include(q => q.Profile)
				.SingleOrDefaultAsync(q => q.UserName.ToLower() == model.UserName.Trim().ToLower());

			if (user == null)
			{
				return CommandHandlerResult.Error("Invalid Username or Password");
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (result.Succeeded)
			{
				var claims = await _userManager.GetClaimsAsync(user);

				var roles = JsonConvert.SerializeObject(user.UserRoles.Select(r => new {
					r.RoleId,
					Role = r.Role?.Name ?? "User"
				}), Formatting.Indented, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
				return CommandHandlerResult.OkDelayed(this, _ => new TokenResponse
				{
					AccessToken = _tokenProvider.GenerateJwtToken(claims),
					RefreshToken = _tokenProvider.GenerateRefreshToken(),
					ExpiresIn = 3600 * 24,
					TokenType = "Bearer",
				});
			}

			if (result.IsLockedOut)
			{
				return CommandHandlerResult.Error("This Account is locked");
			}

			if (result.IsNotAllowed)
			{
				return CommandHandlerResult.Error("This Account is not allowed");
			}

			return CommandHandlerResult.Error("Invalid Username or Password");
		}
	}
}
