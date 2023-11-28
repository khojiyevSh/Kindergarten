using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Teachers.Commands
{
    public class UpdateTeacherHimselfCommand : ICommand<TeacherViewModel>
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public DateTime Bithdate { get; set; } 

        public string? PhoneNumber { get; set; }
    }

    public class UpdateHimselfTeacherCommandHandler : ICommandHandler<UpdateTeacherHimselfCommand, TeacherViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _haskService;
        public UpdateHimselfTeacherCommandHandler(IApplicationDbContext context, IHashService haskService)
        {
            _context = context;
            _haskService = haskService;
        }

        public async Task<TeacherViewModel> Handle(UpdateTeacherHimselfCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id == request.Id,cancellationToken);

            if (teacher == null || !teacher.IsActiveTeacher)
            {
                throw new NotFoundException();
            }

            teacher.FirstName = request.FirstName ?? teacher.FirstName;
            teacher.LastName =request.LastName ?? teacher.LastName;
            teacher.MiddleName =request.MiddleName ?? teacher.MiddleName;
            teacher.Bithdate = request.Bithdate;
            teacher.PhoneNumber = request.PhoneNumber ?? teacher.PhoneNumber;
            teacher.Email = request.Email ?? teacher.Email;
            teacher.User!.Email = request.Email ?? teacher.User.Email;
            teacher.User!.PasswordHash = _haskService.GetHash(request.Password!) ?? teacher.User!.PasswordHash;
            teacher.User.UserName = request.UserName ?? teacher.User.UserName;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return new TeacherViewModel()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                Bithdate = teacher.Bithdate,
                Email = teacher.Email,
                Password = request.Password,
                PhoneNumber=teacher.PhoneNumber,
                Roles =teacher.User.Roles,
                UserId = teacher.User.Id,
                UserName = teacher.User.UserName,
            };
        }
    }
}
