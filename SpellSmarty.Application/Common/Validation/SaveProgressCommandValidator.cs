using FluentValidation;
using SpellSmarty.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellSmarty.Application.Common.Validation
{
    public class SaveProgressCommandValidator : AbstractValidator<SaveProgressQuery>
    {
        public SaveProgressCommandValidator()
        {
            RuleFor(x => x.progress).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.videoId).NotEmpty().GreaterThanOrEqualTo(1);
            RuleFor(x => x.token).NotEmpty();

        }
    }
}
