using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.ChildGroupModels;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.ChildGroupCommands
{
    public class CreateChildGroupCommand : ICommand<ChildGroupViewModel>
    {
        public int ChildernId { get; set; }

        public bool IsActive { get; set; }

        public bool IsPayed { get; set; }

        public int GroupId { get; set; }
    }

    public class CreateChildGroupCommandHandler : ICommandHandler<CreateChildGroupCommand, ChildGroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateChildGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ChildGroupViewModel> Handle(CreateChildGroupCommand request, CancellationToken cancellationToken)
        {

            var childGroup = new Domain.Entities.ChildernGroup()
            {
                ChildernId = request.ChildernId,
                GroupId = request.GroupId,
                IsActive = request.IsActive,
                IsPayed = request.IsPayed,
            };
            await _context.ChildernGroups.AddAsync(childGroup);
            await _context.SaveChangesAsync(cancellationToken);

            var childGroupEntity = await _context.ChildernGroups.Include(x => x.Group).FirstOrDefaultAsync(x => x.GroupId == request.GroupId);

            if (childGroupEntity!.ChildernId == request.ChildernId) 
            {
                throw new Exception("Already Exis");
            }

            if (childGroupEntity == null &&
                childGroupEntity!.Group!.MaxChildCount <= childGroupEntity.Group.IsHowManyCount) 
            {
               
                _context.ChildernGroups.Remove(childGroup);
                throw new NotFoundException(new Exception("Not Yet"));

            }

            ++childGroupEntity.Group!.IsHowManyCount;

            _context.Groups.Update(childGroupEntity.Group!);
            await _context.SaveChangesAsync(cancellationToken);

            return new ChildGroupViewModel()
            {
                ChildernId = childGroup.ChildernId,
                GroupId=childGroup.GroupId,
                IsActive=request.IsActive,
                IsPayed =request.IsPayed
            };
        }
    }
}
