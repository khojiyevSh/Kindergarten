using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.ChildernCommands
{
    public class UpdateChildernCommand : ICommand<ChildernViewModel>
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? FatherNumber { get; set; }

        public string? MatherNumber { get; set; }
    }

    public class UpdateChildernCommandHandler : ICommandHandler<UpdateChildernCommand, ChildernViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;
        public UpdateChildernCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<ChildernViewModel> Handle(UpdateChildernCommand request, CancellationToken cancellationToken)
        {
            var childern =  await _context.Childerns.Include(x=>x.User)
                                                    .FirstOrDefaultAsync(x=>x.Id == request.Id,cancellationToken);

            if (childern == null)
            {
                throw new NotFoundException();
            }

            childern.User!.UserName = request.UserName ?? childern.User.UserName;
            childern.User.PasswordHash = _hashService.GetHash(request.Password!) ?? childern.User.PasswordHash;
            childern.FatherNumber = request.FatherNumber ?? childern.FatherNumber;
            childern.MatherNumber =request.MatherNumber ?? childern.MatherNumber;

            _context.Childerns.Update(childern);
            await _context.SaveChangesAsync(cancellationToken);

            return new ChildernViewModel()
            {
                FirstName = childern.FirstName,
                LastName = childern.LastName,
                MatherNumber = childern.MatherNumber,
                FatherNumber = childern.FatherNumber,
                Bithdate = childern.Bithdate,
                MiddleName = childern.MiddleName,
                UserId = childern.UserId,
                Password = request.Password,
                Id = childern.Id,
                UserName = request.UserName
            };
        }
    }
}
