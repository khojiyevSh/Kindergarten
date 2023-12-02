using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.GroupModels;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupQueries
{
    public class GetGroupQuery : IQuery<GetGroupViewModel>
    {
        public int Id { get; set; }
    }

    public class GetGroupQueryHandler : IQueryHandler<GetGroupQuery, GetGroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetGroupQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetGroupViewModel> Handle(GetGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (group == null || !group.IsActive)
            {
                throw new NotFoundException(new NotActiveException());
            }

            return new GetGroupViewModel()
            {
                Id = group.Id,
                Name = group.Name!,
                StartData = group.StartData,
                EndData  =group.EndData,
                IsActive = group .IsActive,
                MaxChildCount = group.MaxChildCount
            };
        }
    }
}
