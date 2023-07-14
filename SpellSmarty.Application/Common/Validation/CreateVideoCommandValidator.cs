using FluentValidation;
using SpellSmarty.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Validation
{
    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator() 
        {
            RuleFor(f => f.title)
                    .MaximumLength(255).WithMessage("Content must be less than 255 characters")
                .NotEmpty().WithMessage("Content must not be empty");
            RuleFor(f => f.channelname)
                .MaximumLength(255).WithMessage("Content must be less than 255 characters")
                .NotEmpty().WithMessage("Content must not be empty");
            RuleFor(f => f.srcid)
                .NotEmpty().WithMessage("Content must not be empty");
        }
    }
}
