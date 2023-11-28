using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.ChildernQueries
{
    public class GetChildernQuery : IQuery<ChildernViewModel>
    {
        public int Id { get; set; }
    }

    public class GetChildernQueryHandler : IQueryHandler<GetChildernQuery, ChildernViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetChildernQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChildernViewModel> Handle(GetChildernQuery request, CancellationToken cancellationToken)
        {
            var childern = await _context.Childerns.Include(x=>x.User)
                                                   .FirstOrDefaultAsync(x=>x.Id == request.Id,cancellationToken);

            if (childern == null || !childern.IsActiveChildern)
            {
                throw new NotFoundException();
            }

            return  new ChildernViewModel()
            {
                FirstName = childern.FirstName,
                LastName = childern.LastName,
                MatherNumber = childern.MatherNumber,
                FatherNumber = childern.FatherNumber,
                Bithdate = childern.Bithdate,
                MiddleName = childern.MiddleName,
                UserId =childern.User!.Id,
                Id = childern.Id,
                UserName =childern.User.UserName
            };
        }
    }
}
