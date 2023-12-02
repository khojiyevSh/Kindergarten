using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.ChildGroupCommands
{
    public class DeleteChildGroupCommand : ICommand<int>
    {
        public int ChildGroupId { get; set; }
    }


    public class DeleteChildGroupCommandHandler : ICommandHandler<DeleteChildGroupCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteChildGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteChildGroupCommand request, CancellationToken cancellationToken)
        {
            var childGroup = await _context.ChildernGroups.FirstOrDefaultAsync(x=>x.Id == request.ChildGroupId);

            if (childGroup == null)
            {
               throw new NotFoundException();
            }

            childGroup.IsActive = false;

            _context.ChildernGroups.Update(childGroup);
            await _context.SaveChangesAsync(cancellationToken);

            return childGroup.Id;
        }
    }
}
