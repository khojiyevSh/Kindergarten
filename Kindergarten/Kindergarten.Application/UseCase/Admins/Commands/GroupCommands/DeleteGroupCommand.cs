using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.GroupCommands
{
    public class DeleteGroupCommand : ICommand<int>
    {
        public int GruopId { get; set; }
    }

    public class DeleteGroupCommandHandler : ICommandHandler<DeleteGroupCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var groupPrices = await _context.GroupPrices!.Include(x=>x.Group).Where(x=>x.GroupId == request.GruopId).ToListAsync();
            var trainingTimes = await _context.TrainingTimes!.Where(x=>x.GroupId == request.GruopId).ToListAsync();

            if (groupPrices == null && trainingTimes == null)
            {
                throw new NotFoundException();
            }

           foreach (var groupPrice in groupPrices)
            {
                groupPrice.IsActive = false;

                if (groupPrice.Group!.IsActive)
                {
                   groupPrice.Group!.IsActive = false;
                }
            }

            foreach (var trainingTime in trainingTimes)
            {
                if (!trainingTime.IsTrainningTime)
                {
                    _context.TrainingTimes.Remove(trainingTime);
                }

            }

            _context.GroupPrices!.UpdateRange(groupPrices);

            await _context.SaveChangesAsync(cancellationToken);

            return request.GruopId;
        }
    }
}
