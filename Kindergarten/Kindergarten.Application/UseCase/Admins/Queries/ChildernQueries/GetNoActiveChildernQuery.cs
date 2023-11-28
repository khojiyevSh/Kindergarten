using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.ChildernQueries
{
    public class GetNoActiveChildernQuery : IQuery<ChildernViewModel>
    {
        public int Id { get; set; }
    }

    public class GetNoActiveChildernQueryHandler : IQueryHandler<GetNoActiveChildernQuery, ChildernViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetNoActiveChildernQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChildernViewModel> Handle(GetNoActiveChildernQuery request, CancellationToken cancellationToken)
        {
            var childern = await _context.Childerns.Include(u => u.User)
                                                 .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (childern == null || childern.IsActiveChildern)
            {
                throw new NotFoundException();
            }

            return new ChildernViewModel()
            {
                Bithdate = childern!.Bithdate,
                FirstName = childern.FirstName,
                LastName = childern.LastName,
                MiddleName = childern.MiddleName,
                MatherNumber = childern.MatherNumber,
                FatherNumber = childern.FatherNumber,
                UserId = childern.UserId,
                UserName = childern.User!.UserName,
                Id = request.Id,
            };
        }
    }
}