using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Models.ChildGroupModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Kindergarten.Application.UseCase.Admins.Queries.ChildGroupQueries
{
    public class GetAllChildGroupQuery : IQuery<List<GetChildGroupViewModel>>
    {
    }


    public class GetAllChildGroupQueryHandler : IQueryHandler<GetAllChildGroupQuery, List<GetChildGroupViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllChildGroupQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetChildGroupViewModel>> Handle(GetAllChildGroupQuery request, CancellationToken cancellationToken)
        {
            return await _context.ChildernGroups.Include(x => x.Childern)
                                                           .ThenInclude(x => x.User)
                                                           .Include(x => x.Group)
                                                           .Where(x => x.IsActive)
                                                           .Select(x => new GetChildGroupViewModel()
                                                           {
                                                               ChildGroupId = x.GroupId,
                                                               ChildrenId = x.ChildernId,
                                                               FatherNumber = x.Childern!.FatherNumber,
                                                               MatherNumber = x.Childern.MatherNumber,
                                                               FirstName = x.Childern.FirstName,
                                                               GroupName = x.Group!.Name,
                                                               IsPayed = x.IsPayed,
                                                               Username = x.Childern.User!.UserName

                                                           }).ToListAsync();
                                                           


        }
    }
}
