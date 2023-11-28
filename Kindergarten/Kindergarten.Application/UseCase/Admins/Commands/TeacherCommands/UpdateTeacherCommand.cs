using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Kindergarten.Application.UseCase.Admins.Commands.TeacherCommands
{
    public class UpdateTeacherCommand : ICommand<TeacherViewModel>
    {
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }

    public class UpdateTeacherCommandHandler : ICommandHandler<UpdateTeacherCommand, TeacherViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;
        public UpdateTeacherCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<TeacherViewModel> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Include(v => v.User)
                                                 .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (teacher == null)
            {
                if (!teacher!.IsActiveTeacher)
                {
                    throw new AlreadyDeleteException(new NotFoundException());
                }
                throw new NotFoundException();
            }

            teacher.Email = request.Email ?? teacher.Email;
            teacher.PhoneNumber = request.PhoneNumber ?? teacher.PhoneNumber;
            teacher.User!.UserName = request.UserName ?? teacher.User.UserName;
            teacher.User.PasswordHash = _hashService.GetHash(request.Password!) ?? teacher.User.PasswordHash;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return new TeacherViewModel()
            {
                Bithdate = teacher.Bithdate,
                Email = teacher.Email,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                MiddleName = teacher.MiddleName,
                Password = request.Password,
                Roles = Roles.Teacher,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName,
                UserId = teacher.UserId,
                Id = teacher.Id
            };
        }
    }
}
