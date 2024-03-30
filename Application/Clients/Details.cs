using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Clients
{
    public class Details
    {
        public class Query : IRequest<Result<ClientDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ClientDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<Result<ClientDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients
                  .ProjectTo<ClientDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<ClientDto>.Success(client);
            }
        }
    }
}