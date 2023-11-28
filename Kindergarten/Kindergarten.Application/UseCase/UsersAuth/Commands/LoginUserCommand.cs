using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.UsersAuth.Commands
{
    public class LoginUserCommand : ICommand<string>
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public class LoginUserCommandHendler : ICommandHandler<LoginUserCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHendler(IApplicationDbContext context, IHashService hashService, ITokenService tokenService)
        {
            _context = context;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
           var user = await _context.Users.FirstOrDefaultAsync(x=>x.UserName ==  request.Username, cancellationToken);

            if (user == null || !user.IsActiveUser) 
            {
                throw new LoginException(new UserNotFoundException(nameof(user)));
            }

            if(user.PasswordHash !=_hashService.GetHash(request.Password!)) 
            {
                throw new LoginException();
            }

            return _tokenService.GetToken(user);
        }
    }
}
