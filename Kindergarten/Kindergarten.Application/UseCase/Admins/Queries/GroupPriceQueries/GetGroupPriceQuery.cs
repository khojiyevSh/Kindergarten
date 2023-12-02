using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.GroupPricesModel;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.GroupPriceQueries
{
    public class GetGroupPriceQuery : IQuery<GetGroupPriceViewModel>
    {
        public int Id { get; set; }
    }

    public class GetGroupPriceQueryHandler : IQueryHandler<GetGroupPriceQuery,GetGroupPriceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetGroupPriceQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetGroupPriceViewModel> Handle(GetGroupPriceQuery request, CancellationToken cancellationToken)
        {
            var groupPrice = await _context.GroupPrices
                                            .Include(x=>x.Group)
                                            .FirstOrDefaultAsync(x=>x.Id == request.Id);

            if (groupPrice == null)
            {
                throw new NotFoundException();
            }

            return new GetGroupPriceViewModel()
            {
                AgeStatus = groupPrice.AgeStatus,
                CategotyGroup = groupPrice.CategotyGroup,
                GroupId = groupPrice.GroupId,
                GroupName = groupPrice.Group!.Name!,
                Id = groupPrice.Id,
                IsActive = groupPrice.IsActive,
                Monthdate = groupPrice.Monthdate,
                Price = groupPrice.Price,
            };
        }
    }
}
