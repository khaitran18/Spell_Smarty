using FluentValidation;
using SpellSmarty.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Validation
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(8).WithMessage("Username must be at least 8 characters long.")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(50).WithMessage("Password cannot exceed 50 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
            .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, and one digit.");

            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is in incorrect format");
        }
    }
}
