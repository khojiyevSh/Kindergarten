using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.ChildernQueries
{
    public class GetAllNoActiveChildernQuery : ICommand<List<ChildernViewModel>>
    {

    }

    public class GetAllNoActionsqueryHandler : ICommandHandler<GetAllNoActiveChildernQuery, List<ChildernViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllNoActionsqueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChildernViewModel>> Handle(GetAllNoActiveChildernQuery request, CancellationToken cancellationToken)
        {
            var childerns = await _context.Childerns.Include(x=>x.User)
                                                    .Where(x=>x.IsActiveChildern == false).ToListAsync();

            if (childerns == null)
            {
                throw new NotFoundException();
            }

            var childernList = new List<ChildernViewModel>();

            foreach (var children in childernList)
            {
                var a = new ChildernViewModel()
                {
                    Bithdate = children!.Bithdate,
                    FirstName = children.FirstName,
                    LastName = children.LastName,
                    MiddleName = children.MiddleName,
                    MatherNumber = children.MatherNumber,
                    FatherNumber = children.FatherNumber,
                    UserId = children.UserId,
                    UserName = children.UserName,
                    Id = children.Id,
                };
            }
            return childernList;
        }
    }
}
