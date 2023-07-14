using FluentValidation;
using SpellSmarty.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Validation
{
    public class UpdateVideoCommandValidator : AbstractValidator<UpdateVideoCommand>
    {
        public UpdateVideoCommandValidator() 
        {
            RuleFor(f => f.Title)
                        .MaximumLength(255).WithMessage("Content must be less than 255 characters")
        }
    }
}
