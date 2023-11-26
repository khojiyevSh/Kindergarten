using Kindergarten.Application.Abstractions;
using Kindergarten.Infrastucture.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Infrastucture.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IHashService _hashService;

        public AuthService(ITokenService tokenService, IHashService hashService, IApplicationDbContext context)
        {
            _tokenService = tokenService;
            _hashService = hashService;
            _context = context;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(x => x.UserName == username);

            if (user == null) 
            {
                throw new Exception("User Not Found");
            }

            else if (user.PasswordHash != _hashService.GetHash(password))
            {
                throw new Exception("Password is wrong");
            }

            return _tokenService.GetToken(user);
        }
    }
}
