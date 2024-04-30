using Domain;
using FluentValidation;

namespace Application.Manufacturers
{
    public class ManufacturerValidator : AbstractValidator<Manufacturer>
    {
        public ManufacturerValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}