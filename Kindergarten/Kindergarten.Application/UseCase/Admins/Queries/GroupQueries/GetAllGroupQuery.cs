using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupQueries
{
    public class GetAllGroupQuery : IQuery<List<GetGroupViewModel>>
    {

    }

    public class GetAllGroupQueryHandler : IQueryHandler<GetAllGroupQuery, List<GetGroupViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllGroupQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetGroupViewModel>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups.Where(x => x.IsActive == true).ToListAsync();

            if (groups == null)
            {
              throw new  NotFoundException();
            }

            var groupList = new List<GetGroupViewModel>();

            foreach (var group in groups)
            {
                groupList.Add(new GetGroupViewModel
                {
                    Id = group.Id,
                    EndData = group.EndData,
                    StartData= group.StartData,
                    MaxChildCount= group.MaxChildCount,
                    IsActive =group.IsActive,
                    Name=group.Name!
                });
            }

            return groupList;
        }
    }
}
