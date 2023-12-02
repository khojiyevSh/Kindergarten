using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Models.GroupPricesModel;
using Kindergarten.Application.UseCase.Admins.Queries.GroupPriceQueries;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupPriceQueries
{
    public class GetAllGroupPriceGroupIdQuery : IQuery<List<GetGroupPriceViewModel>>
    {
        public int GroupId { get; set; }
    }


    public class GetAllGroupPriceGroupIdQueryHandler : IQueryHandler<GetAllGroupPriceGroupIdQuery, List<GetGroupPriceViewModel>>
    {
       private readonly IApplicationDbContext _context;

       public GetAllGroupPriceGroupIdQueryHandler(IApplicationDbContext context)
       {
         _context = context;
       }

        public async Task<List<GetGroupPriceViewModel>> Handle(GetAllGroupPriceGroupIdQuery request, CancellationToken cancellationToken)
        {
                   return await  _context.GroupPrices.Include(x => x.Group).Where(x=>x.GroupId == request.GroupId)
                                                     .Select(x => new GetGroupPriceViewModel()
                                                     {
                                                         AgeStatus = x.AgeStatus,
                                                         CategotyGroup = x.CategotyGroup,
                                                         GroupId = x.GroupId,
                                                         GroupName = x.Group.Name!,
                                                         Id = x.Id,
                                                         IsActive = x.IsActive,
                                                         Monthdate = x.Monthdate,
                                                         Price = x.Price,
                                                     }).ToListAsync();
        }
    }
}
