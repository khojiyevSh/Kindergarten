using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.ChildGroupModels;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.ChildGroupCommands
{
    public class UpdateChildGroupCommand : ICommand<ChildGroupViewModel>
    {
        public int ChildId { get; set; }

        public bool IsActive { get; set; }

        public bool IsPayed { get; set; }
    }


    public class UpdateChildGroupCommandHandler : ICommandHandler<UpdateChildGroupCommand, ChildGroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public UpdateChildGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChildGroupViewModel> Handle(UpdateChildGroupCommand request, CancellationToken cancellationToken)
        {
            var childGroup = await _context.ChildernGroups.FirstOrDefaultAsync(x=>x.ChildernId ==  request.ChildId);

            if (childGroup == null)
            {
               throw new NotFoundException();
            }

            childGroup.IsPayed = request.IsPayed;
            childGroup.IsActive = request.IsActive;

            _context.ChildernGroups.Update(childGroup);
            await _context.SaveChangesAsync(cancellationToken);

            return new ChildGroupViewModel()
            {
              ChildernId = childGroup.Id,
              GroupId = childGroup.GroupId,
              IsActive = childGroup.IsActive,
              IsPayed = childGroup.IsPayed,
            };
        }
    }
}
