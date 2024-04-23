using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto.Validations
{
    public sealed class ContentTypeDtoValidator : AbstractValidator<ContentTypeDto>
    {
        public ContentTypeDtoValidator()
        {
            RuleFor(u => u.TypeName).NotNull()
                .MaximumLength(50);
        }
    }
}
