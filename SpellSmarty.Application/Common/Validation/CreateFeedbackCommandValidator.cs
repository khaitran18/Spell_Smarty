using FluentValidation;
using SpellSmarty.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Validation
{
    public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
    {
        public CreateFeedbackCommandValidator()
        {
            RuleFor(f => f.videoId)
                .GreaterThanOrEqualTo(1).WithMessage("Invalid video id")
                .NotEmpty().WithMessage("Video id must not be empty");
            RuleFor(f => f.content)
                .MaximumLength(255).WithMessage("Content must be less than 255 characters")
                .MinimumLength(3).WithMessage("Content must be more than 3 characters")
                .NotEmpty().WithMessage("Content must not be empty");
        }
    }
}
