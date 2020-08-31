using AccountService.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AccountService.Commands
{
	public class SignUpCommandHandler : IHandleCommand<SignUpCommand>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IConfiguration _configuration;
		public SignUpCommandHandler(UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IConfiguration configuration)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_configuration = configuration;
		}
		public async Task<CommandHandlerResult> HandleAsync(SignUpCommand command)
		{
			var model = command.Model;
			var userName = model.Email.ToUpper().Substring(0, model.Email.ToLower().IndexOf('@'));
			var user = new User
			{
				UserName = model.Email,
				Email = model.Email,
				Profile = new Profile
				{
					CompanyName = model.CompanyName ?? userName,
					Website = model.Website,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Email = model.Email.ToLower()
				},
			};

			user.Profile.CreatedBy = user;

			var createUserResult = await _userManager.CreateAsync(user, model.Password);

			if (!createUserResult.Succeeded)
			{
				return CommandHandlerResult.Error(createUserResult.Errors?.FirstOrDefault()?.Description);
			}

			var addClaimsResult = await _userManager.AddClaimsAsync(user, new Claim[] {
				new Claim (JwtClaimTypes.Id, user.Id.ToString ()),
				new Claim (JwtClaimTypes.Subject, user.Id.ToString ()),
				new Claim (JwtClaimTypes.Email, user.Email),
				new Claim (JwtClaimTypes.Name, user.Profile?.FullName),
				new Claim (JwtClaimTypes.Role, model.RoleName)
			});

			if (!addClaimsResult.Succeeded)
			{
				return CommandHandlerResult.Error(addClaimsResult.Errors?.FirstOrDefault()?.Description);
			}

			string roleName = model.RoleName.ToLower();

			bool roleExists = await _roleManager.RoleExistsAsync(roleName);

			if (!roleExists)
			{
				var createRoleResult = await _roleManager.CreateAsync(new Role(roleName));

				if (!createRoleResult.Succeeded)
				{
					return CommandHandlerResult.Error(createRoleResult.Errors?.FirstOrDefault()?.Description);
				}
			}

			var addToRoleResult = await _userManager.AddToRoleAsync(user, roleName);

			if (!addToRoleResult.Succeeded)
			{
				return CommandHandlerResult.Error(addToRoleResult.Errors?.FirstOrDefault()?.Description);
			}

			string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			string encodedToken = HttpUtility.UrlEncode(token);

			string emailConfirmationLink = $"{_configuration["EmailSettings:EmailConfirmationUrl"]}?userid={user.Id.ToString()}&token={encodedToken}";

			return CommandHandlerResult.OkDelayed(this, _ => new
			{
				UserId = user.Id.ToString(),
				Link = emailConfirmationLink
			});
		}
	}
}
