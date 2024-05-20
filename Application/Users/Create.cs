using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Core;
using Domain;
using FluentValidation;

namespace Application.Users
{
    public class Create
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
            private readonly IMapper _mapper;

            public Handler(UserManager<AppUser> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<AppUser>(request.User);
                user.Role = "User";
                user.IsActive = true;
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;

                var result = await _userManager.CreateAsync(user, "Pa$$w0rd");

                if (!result.Succeeded) return Result<Unit>.Failure("Failed to create user");

                await _userManager.AddToRoleAsync(user, user.Role);
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
