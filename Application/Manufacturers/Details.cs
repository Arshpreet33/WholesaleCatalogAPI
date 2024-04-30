using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Manufacturers
{
    public class Details
    {
        public class Query : IRequest<Result<ManufacturerDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ManufacturerDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;

            }
            public async Task<Result<ManufacturerDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var client = await _context.Clients
                  .ProjectTo<ManufacturerDto>(_mapper.ConfigurationProvider)
                  .FirstOrDefaultAsync(x => x.Id == request.Id);

                return Result<ManufacturerDto>.Success(client);
            }
        }
    }
}