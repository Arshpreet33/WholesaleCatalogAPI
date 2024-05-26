using Domain;
using MediatR;
using Application.Core;
using Microsoft.AspNetCore.Identity;

namespace Application.Users
{
    public class ToggleActive
    {
        public class Command : IRequest<Result<Unit>>
        {
            public string UserName { get; set; }
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
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null) return null;

                // Enable/Disable the user's account
                user.LockoutEnd = user.IsActive ? (DateTimeOffset?)DateTime.UtcNow.AddYears(100) : null;
                user.IsActive = !user.IsActive;
                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded) return Result<Unit>.Failure("Failed to activate/deactivate user");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
