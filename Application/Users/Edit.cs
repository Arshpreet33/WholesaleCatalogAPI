using MediatR;
using Domain;
using AutoMapper;
using Application.Core;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Application.Users
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public UserDto User { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.User).SetValidator(new UserValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.User.UserName);

                if (user == null) return null;

                user.DisplayName = request.User.DisplayName ?? user.DisplayName;
                user.Bio = request.User.Bio ?? user.Bio;
                user.Email = request.User.Email ?? user.Email;
                user.IsActive = true;
                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded) return Result<Unit>.Failure("Failed to update user");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
