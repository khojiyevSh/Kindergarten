using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.GroupPricesModel;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupPriceQueries
{
    public class GetAllGroupPriceQuery : IQuery<List<GetGroupPriceViewModel>>
    {
    }

    public class GetAllGroupPriceQueryHandler : IQueryHandler<GetAllGroupPriceQuery, List<GetGroupPriceViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllGroupPriceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetGroupPriceViewModel>> Handle(GetAllGroupPriceQuery request, CancellationToken cancellationToken)
        {
            return await _context.GroupPrices.Include(x=>x.Group)
                                                         .Select(x => new GetGroupPriceViewModel()
                                                         {
                                                             
                                                                 AgeStatus = x.AgeStatus,
                                                                 CategotyGroup = x.CategotyGroup,
                                                                 GroupId = x.GroupId,
                                                                 GroupName = x.Group!.Name!,
                                                                 Id = x.Id,
                                                                 IsActive = x.IsActive,
                                                                 Monthdate = x.Monthdate,
                                                                 Price = x.Price,

                                                             
                                                         }).ToListAsync();
        }
    }
}
