using AccountService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Commands
{
	public class SignInCommand : CommandBase
	{
		public SignInCommand(SignInModel signInModel)
		{
			Model = signInModel;
		}

		protected override IEnumerable<string> OnValidation()
		{
			if (string.IsNullOrEmpty(Model.UserName))
			{
				yield return $"{nameof(Model.UserName)} is required";
			}
			if (string.IsNullOrEmpty(Model.Password))
			{
				yield return $"{nameof(Model.Password)} is required";
			}
		}

		public SignInModel Model { get; }
	}
}
