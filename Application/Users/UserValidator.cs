using Domain;
using FluentValidation;

namespace Application.Users
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Bio).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
        }
    }
}
