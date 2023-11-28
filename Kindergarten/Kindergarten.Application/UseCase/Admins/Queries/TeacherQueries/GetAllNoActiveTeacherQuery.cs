using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.TeacherQueries
{
    public class GetAllNoActiveTeacherQuery : IQuery<List<TeacherViewModel>>
    {

    }

    public class GetAllNoActiveTeacherQueryHandler : IQueryHandler<GetAllNoActiveTeacherQuery, List<TeacherViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllNoActiveTeacherQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TeacherViewModel>> Handle(GetAllNoActiveTeacherQuery request, CancellationToken cancellationToken)
        {
            var teacherList = await _context.Teachers.Include(x => x.User)
                                                      .Where(x => x.IsActiveTeacher == false)
                                                      .ToListAsync();

            if (teacherList == null)
            {
                throw new NotFoundException();
            }

            var teachers = new List<TeacherViewModel>();

            foreach (var teacher in teacherList)
            {
                var teacherView = new TeacherViewModel
                {
                    Bithdate = teacher.Bithdate,
                    Email = teacher.Email,
                    FirstName = teacher.FirstName,
                    Id = teacher.Id,
                    LastName = teacher.LastName,
                    MiddleName = teacher.MiddleName,
                    PhoneNumber = teacher.PhoneNumber,
                    UserName = teacher.User!.UserName,
                    Roles = teacher.User.Roles,
                    UserId = teacher.User.Id
                };
                teachers.Add(teacherView);
            }

            return teachers;
        }
    
    }
}
