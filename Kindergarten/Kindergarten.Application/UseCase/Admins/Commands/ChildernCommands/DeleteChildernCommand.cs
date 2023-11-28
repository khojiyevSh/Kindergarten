using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.ChildernCommands
{
    public class DeleteChildernCommand : ICommand<int>
    {
      public int Id { get; set; }
    }

    public class DeleteChildernCommandHandler : ICommandHandler<DeleteChildernCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteChildernCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteChildernCommand request, CancellationToken cancellationToken)
        {
            var childern = await _context.Childerns.Include(u=>u.User)
                                                   .FirstOrDefaultAsync(x=>x.Id == request.Id,cancellationToken);

            if (childern == null)
            {
                if(!childern!.IsActiveChildern)
                {
                    throw new AlreadyDeleteException(new NotFoundException());
                }
                throw new NotFoundException();
            }

            childern.IsActiveChildern = false;
            childern.User!.IsActiveUser = false;

            _context.Childerns.Update(childern);
            await _context.SaveChangesAsync(cancellationToken);

            return childern.Id;
        }
    }
}
