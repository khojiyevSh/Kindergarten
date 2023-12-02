using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Models;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Kindergarten.Application.UseCase.Admins.Commands.TeacherCommands
{
    public class CreateTeacherCommand : ICommand<TeacherViewModel>
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public DateTime Bithdate { get; set; }

        public string? PhoneNumber { get; set; }
    }

    public class CreateTeacherCommandHandler : ICommandHandler<CreateTeacherCommand, TeacherViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHashService _hashService;

        public CreateTeacherCommandHandler(IApplicationDbContext context, IHashService hashService)
        {
            _context = context;
            _hashService = hashService;
        }

        public async Task<TeacherViewModel> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Email = request.Email,
                PasswordHash = _hashService.GetHash(request.Password!),
                UserName = request.UserName,
                Roles = Roles.Teacher,
                IsActiveUser = true
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var teacher = new Teacher()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Email = request.Email,
                Bithdate = request.Bithdate,
                PhoneNumber = request.PhoneNumber,
                UserId = user.Id,
                IsActiveTeacher = true
            };

            await _context.Teachers.AddAsync(teacher, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new TeacherViewModel
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Email = request.Email,
                Bithdate = request.Bithdate,
                PhoneNumber = request.PhoneNumber,
                UserId = user.Id,
                Password = request.Password,
                Roles = Roles.Teacher,
                UserName = request.UserName,
                Id = teacher.Id
            };
        }
    }
}
