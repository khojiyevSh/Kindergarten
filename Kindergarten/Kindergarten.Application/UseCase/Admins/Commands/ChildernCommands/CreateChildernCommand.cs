using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Models;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kindergarten.Application.UseCase.Admins.Commands.ChildernCommands
{
    public class CreateChildernCommand : ICommand<ChildernViewModel>
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? FatherNumber { get; set; }

        public string? MatherNumber { get; set; }

        public DateTime Bithdate { get; set; }
    }

    public class CreateChildernCommandHandler : ICommandHandler<CreateChildernCommand, ChildernViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public CreateChildernCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<ChildernViewModel> Handle(CreateChildernCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
               UserName = request.UserName,
               PasswordHash = _hashService.GetHash(request.Password!),
               Roles = Roles.Childern,
               IsActiveUser = true
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(cancellationToken);

            var childern = new Childern()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MatherNumber = request.MatherNumber,
                FatherNumber = request.FatherNumber,
                Bithdate = request.Bithdate,
                MiddleName = request.MiddleName,
                UserId = user.Id,
                IsActiveChildern = true
            };

            await _context.Childerns.AddAsync(childern);
            await _context.SaveChangesAsync(cancellationToken);

            return new ChildernViewModel()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MatherNumber = request.MatherNumber,
                FatherNumber = request.FatherNumber,
                Bithdate = request.Bithdate,
                MiddleName = request.MiddleName,
                UserId = user.Id,
                Password =request.Password,
                Id = childern.Id,
                UserName = user.UserName
            };
        }
    }
}
