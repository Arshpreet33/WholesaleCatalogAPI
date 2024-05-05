using Domain;
using FluentValidation;

namespace Application.Categories
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}