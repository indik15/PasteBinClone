using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto.Validations
{
    public class PasteDtoValidator : AbstractValidator<PasteDto>
    {
        public PasteDtoValidator()
        {
            RuleFor(u => u.Body).NotEmpty();
            RuleFor(u => u.Title).NotEmpty().MaximumLength(75);
        }
    }
}
