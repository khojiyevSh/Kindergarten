using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.GroupPrices
{
    public class DeleteGroupPriceMonthCommand : ICommand<int>
    {
        public int Id { get; set; }
    }

    public class DeleteGroupPriceMonthCommandHandler : ICommandHandler<DeleteGroupPriceMonthCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGroupPriceMonthCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteGroupPriceMonthCommand request, CancellationToken cancellationToken)
        {
            var groupPrise = await _context.GroupPrices.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (groupPrise == null)
            {
                throw new NotFoundException();
            }

            groupPrise.IsActive = false;

            _context.GroupPrices.Update(groupPrise);

            var trainingTimes = await _context.TrainingTimes.Where(x => x.GroupId == groupPrise.GroupId).ToListAsync();

            foreach (var trainingTime in trainingTimes)
            {
                if(trainingTime.StartTime.Month == groupPrise.Monthdate.Month && !trainingTime.IsTrainningTime)
                {
                   _context.TrainingTimes.Remove(trainingTime);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }
    }
}
