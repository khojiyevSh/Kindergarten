using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.GroupModels;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupQueries
{
    public class GetAllNoActiveGroupQuery : IQuery<List<GetGroupViewModel>>
    {

    }

    public class GetAllNoActiveGroupQueryHandler : IQueryHandler<GetAllNoActiveGroupQuery, List<GetGroupViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllNoActiveGroupQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetGroupViewModel>> Handle(GetAllNoActiveGroupQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups.Where(x => x.IsActive == false).ToListAsync();

            if (groups == null)
            {
                throw new NotFoundException();
            }

            var groupList = new List<GetGroupViewModel>();

            foreach (var group in groups)
            {
                groupList.Add(new GetGroupViewModel
                {
                    Id = group.Id,
                    EndData = group.EndData,
                    StartData = group.StartData,
                    MaxChildCount = group.MaxChildCount,
                    IsActive = group.IsActive,
                    Name = group.Name!
                });
            }

            return groupList;
        }
    }
}
