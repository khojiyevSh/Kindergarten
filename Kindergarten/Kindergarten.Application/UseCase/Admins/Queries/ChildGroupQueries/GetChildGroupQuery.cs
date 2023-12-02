using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.ChildGroupModels;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.ChildGroupQueries
{
    public class GetChildGroupQuery : IQuery<GetChildGroupViewModel>
    {
        public int ChildId { get; set; }
    }


    public class GetChildGroupQueryHandler : IQueryHandler<GetChildGroupQuery, GetChildGroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetChildGroupQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetChildGroupViewModel> Handle(GetChildGroupQuery request, CancellationToken cancellationToken)
        {
            var ChildGroup = await _context.ChildernGroups
                                           .Include(x => x.Childern)
                                           .ThenInclude(x => x!.User)
                                           .Include(g => g.Group)
                                           .FirstOrDefaultAsync(x => x.ChildernId == request.ChildId);
                                         
            if (ChildGroup == null)
            {
                throw new NotFoundException();
            }

            return new GetChildGroupViewModel()
            {
                ChildGroupId = ChildGroup.GroupId,
                ChildrenId = ChildGroup.ChildernId,
                FatherNumber = ChildGroup.Childern!.FatherNumber,
                MatherNumber = ChildGroup.Childern.MatherNumber,
                FirstName = ChildGroup.Childern.FirstName,
                GroupName = ChildGroup.Group!.Name,
                IsPayed = ChildGroup.IsPayed,
                Username = ChildGroup.Childern.User!.UserName,
            };
        }
    }
}
