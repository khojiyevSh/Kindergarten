using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupQueries
{
    public class GetNoActiveGroupQuery : IQuery<GetGroupViewModel>
    {
        public string Name { get; set; }
    }

    public class GetNoActiveGroupQueryHandler : IQueryHandler<GetNoActiveGroupQuery, GetGroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetNoActiveGroupQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetGroupViewModel> Handle(GetNoActiveGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

            if (group == null || group.IsActive)
            {
                throw new NotFoundException();
            }

            return new GetGroupViewModel()
            {
                Id = group.Id,
                Name = group.Name!,
                StartData = group.StartData,
                EndData = group.EndData,
                IsActive = group.IsActive,
                MaxChildCount = group.MaxChildCount
            };
        }
    }
}
