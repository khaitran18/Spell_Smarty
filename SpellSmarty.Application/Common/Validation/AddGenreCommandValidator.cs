using FluentValidation;
using SpellSmarty.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Validation
{
    public class AddGenreCommandValidator : AbstractValidator<AddGenreCommand>
    {
        public AddGenreCommandValidator() 
        {
            RuleFor(f => f.genrename)
                .MaximumLength(255).WithMessage("Content must be less than 255 characters")
                .NotEmpty().WithMessage("Video id must not be empty");
        }
    }
}
