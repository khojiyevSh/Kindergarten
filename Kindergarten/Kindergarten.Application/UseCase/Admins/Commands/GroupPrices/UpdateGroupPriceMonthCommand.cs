using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.GroupPricesModel;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.GroupPrices
{
    public class UpdateGroupPriceMonthCommand :ICommand<List<GroupPriceMonthViewModel>>
    {
        public DateTime MonthDate { get; set; }

        public decimal NewPrice { get; set; }
    }

    public class UptateGroupPriceMonthCommandHandler : ICommandHandler<UpdateGroupPriceMonthCommand, List<GroupPriceMonthViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public UptateGroupPriceMonthCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<GroupPriceMonthViewModel>> Handle(UpdateGroupPriceMonthCommand request, CancellationToken cancellationToken)
        {
            var groupPrices = await _context.GroupPrices
                                          .Where(x=>x.Monthdate == request.MonthDate && x.IsActive == true)
                                          .ToListAsync();

            if (groupPrices.Count == 0) 
            {
                throw new NotFoundException();
            }

            var newGroupPrices = new List<GroupPriceMonthViewModel>();

            foreach (var groupPrice in groupPrices)
            {
                if (groupPrice.Monthdate.Month == request.MonthDate.Month)
                {
                    groupPrice.Price = request.NewPrice;

                    _context.GroupPrices.Update(groupPrice);
                    await _context.SaveChangesAsync(cancellationToken);

                    newGroupPrices.Add(new GroupPriceMonthViewModel()
                    {
                        AgeStatus = groupPrice.AgeStatus,
                        CategotyGroup = groupPrice.CategotyGroup,
                        GroupId = groupPrice.GroupId,
                        Id = groupPrice.Id,
                        Monthdate = request.MonthDate,
                        Price = groupPrice.Price,
                        IsActive = groupPrice.IsActive
                    });
                    
                }
            }

            return newGroupPrices;
        }
    }
}
