using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.ChildernQueries
{
    public class GetAllChildernQuery : ICommand<List<ChildernViewModel>>
    {

    }

    public class GetAllChildernQueryHandler : ICommandHandler<GetAllChildernQuery, List<ChildernViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllChildernQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChildernViewModel>> Handle(GetAllChildernQuery request, CancellationToken cancellationToken)
        {
            var childerns = await _context.Childerns.Include(x=>x.User)
                                                   .Where(x=>x.IsActiveChildern == true)
                                                   .ToListAsync();
            if (childerns == null)
            {
                throw new NotFoundException();
            }

            var childernList = new List<ChildernViewModel>();

            foreach (var child in childerns)
            {
                childernList.Add(new ChildernViewModel()
                {
                    FirstName = child.FirstName,
                    LastName = child.LastName,
                    MatherNumber = child.MatherNumber,
                    FatherNumber = child.FatherNumber,
                    Bithdate = child.Bithdate,
                    MiddleName = child.MiddleName,
                    UserId = child.User!.Id,
                    Id = child.Id,
                    UserName =child.User.UserName
                });
            }

            return childernList;
        }
    }
}
