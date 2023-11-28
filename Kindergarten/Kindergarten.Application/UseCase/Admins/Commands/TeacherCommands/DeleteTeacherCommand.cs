using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.TeacherCommands
{
    public class DeleteTeacherCommand : ICommand<int>
    {
        public int Id { get; set; }
    }

    public class DeleteTeacherCommandHandler : ICommandHandler<DeleteTeacherCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTeacherCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _context.Teachers.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (teacher == null)
            {
                throw new NotFoundException();
            }

            teacher.IsActiveTeacher = false;
            teacher.User!.IsActiveUser = false;

            _context.Teachers.Update(teacher);
            await _context.SaveChangesAsync(cancellationToken);

            return teacher.Id;
        }
    }
}
