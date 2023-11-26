using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Abstractions
{
    public interface ITokenService
    {
        string GetToken(User user);
    }
}
