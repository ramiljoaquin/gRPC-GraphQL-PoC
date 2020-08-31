using AccountService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Commands
{
	public class SignUpCommand : CommandBase
	{
		public SignUpCommand(SignUpModel signUpModel)
		{
			Model = signUpModel;
		}

		public SignUpModel Model { get; }

		protected override IEnumerable<string> OnValidation()
		{
            if (string.IsNullOrEmpty(Model.FirstName))
            {
                yield return $"{nameof(Model.FirstName)} is required";
            }
            if (string.IsNullOrEmpty(Model.LastName))
            {
                yield return $"{nameof(Model.LastName)} is required";
            }
            if (string.IsNullOrEmpty(Model.Email))
            {
                yield return $"{nameof(Model.Email)} is required";
            }
            if (!new EmailAddressAttribute().IsValid(Model.Email))
            {
                yield return $"{nameof(Model.Email)} is invalid";
            }
            if (string.IsNullOrEmpty(Model.Password))
            {
                yield return $"{nameof(Model.Password)} is required";
            }
        }
	}
}
