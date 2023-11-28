using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Childerns.Commands
{
    public class UpdateChildernHimselfCommand : ICommand<ChildernViewModel>
    {
        public string? OldUserName { get; set; }

        public string? OldPassword { get; set; }

        public string? NewUserName { get; set; }

        public string? NewPassword { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? FatherNumber { get; set; }

        public string? MatherNumber { get; set; }

        public DateTime Bithdate { get; set; }
    }

    public class UpdateChildernHimselfCommandHandler : ICommandHandler<UpdateChildernHimselfCommand, ChildernViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public UpdateChildernHimselfCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<ChildernViewModel> Handle(UpdateChildernHimselfCommand request, CancellationToken cancellationToken)
        {
            var childern = await _context.Childerns
                  .Include(x => x.User)
                  .FirstOrDefaultAsync(x => x.User!.UserName == request.OldUserName);

            if (childern == null)
            {
                if (!childern!.IsActiveChildern)
                {
                    throw new AlreadyDeleteException(new NotFoundException());
                }
                throw new NotFoundException();
            }

            childern.Bithdate = request.Bithdate;
            childern.FirstName = request.FirstName ?? childern.FirstName;
            childern.LastName = request.LastName ?? childern.LastName;
            childern.MiddleName = request.MiddleName ?? childern.MiddleName;
            childern.FatherNumber =request.FatherNumber ?? childern.FatherNumber;
            childern.MatherNumber = request .MatherNumber ?? childern.MatherNumber;
            childern.User!.UserName = request.NewUserName ?? childern.User.UserName;
            childern.User.PasswordHash = _hashService.GetHash(request.NewPassword!) ?? childern.User.PasswordHash;

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
                Password = request.NewPassword,
                Id = childern.Id,
                UserName = request.NewUserName
            };

        }
    }
}
