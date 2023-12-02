using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.Attendences;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Teachers.Commands
{
    public class CreateAttendenceTimeCommand : ICommand<AttendenceViewModel>
    {
        public string? GroupName { get; set; }

        public DateTime Today { get; set; } 

        public List<AttendenceListViewModel>? AttendenceChild {  get; set; } 
    }

    public class CreateAttendenceTimeCommandHanddler : ICommandHandler<CreateAttendenceTimeCommand,AttendenceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateAttendenceTimeCommandHanddler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttendenceViewModel> Handle(CreateAttendenceTimeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var trainingTime = await _context.TrainingTimes
                                              .Include(g => g.Group)
                                              .ThenInclude(x => x!.Teacher)
                                              .FirstOrDefaultAsync(x => x.Group!.Name == request.GroupName &&
                                               x.Date.DayOfWeek == request.Today.DayOfWeek &&
                                               x.IsTrainningTime == false);

                if (trainingTime == null)
                {
                    throw new NotFoundException();
                }

                var attendenceList = new List<Attendence>();
                var attendenceChildList = new List<AttendenceListViewModel>();

                foreach (var attendence in request.AttendenceChild!)
                {
                    attendenceList.Add(new Attendence
                    {
                        TrainingTimeId = trainingTime.Id,
                        ChildernId = attendence.ChildernId,
                        Participated = attendence.Participated
                    });

                    attendenceChildList.Add(new AttendenceListViewModel()
                    {
                        ChildernId = attendence.ChildernId,
                        Participated = attendence.Participated,

                    });
                }

                trainingTime.IsTrainningTime = true;
                _context.TrainingTimes.Update(trainingTime);

                await _context.Attendences.AddRangeAsync(attendenceList);
                await _context.SaveChangesAsync(cancellationToken);

                return new AttendenceViewModel()
                {
                    TraningTimeId = trainingTime.Id,
                    GroupName = trainingTime.Group!.Name,
                    Tuday = trainingTime.Date,
                    AttendenceChild = attendenceChildList,
                    TeacherId = trainingTime.Group.Teacher!.Id
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
