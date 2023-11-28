using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.TeacherQueries
{
    public class GetNoActiveTeacherQuery : IQuery<TeacherViewModel>
    {
        public int Id { get; set; }
    }

    public class GetNoActiveTeacherQueryHandler : IQueryHandler<GetNoActiveTeacherQuery, TeacherViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetNoActiveTeacherQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherViewModel> Handle(GetNoActiveTeacherQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Include(u => u.User)
                                                 .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (teacher == null || teacher.IsActiveTeacher)
            {
                
                  throw new NotFoundException();
                
            }

            return new TeacherViewModel()
            {
                Bithdate = teacher!.Bithdate,
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                PhoneNumber = teacher.PhoneNumber,
                Roles = teacher.User!.Roles,
                UserId = teacher.UserId,
                UserName = teacher.User.UserName,
                Id = request.Id,
            };
        }
    
    }
}
