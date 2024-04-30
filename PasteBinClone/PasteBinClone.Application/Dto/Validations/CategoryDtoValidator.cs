using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Dto.Validations
{
    public sealed class CategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CategoryDtoValidator()
        {
            RuleFor(u => u.CategoryName).NotEmpty()
                .MaximumLength(50);
        }
    }
}
