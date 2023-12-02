

using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.Attendences;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.AttendenceQueries
{
    public class GetAttendenceDataQuery :IQuery<AttendenceViewModel>
    {
        public string? ChildName { get; set; }

        public DateTime Data { get; set; }
    }


    public class GetAttendenceChildNameQueryHandler : IQueryHandler<GetAttendenceDataQuery, AttendenceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetAttendenceChildNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttendenceViewModel> Handle(GetAttendenceDataQuery request, CancellationToken cancellationToken)
        {
            var attendenceChild = await _context.Attendences
                                              .Include(x => x.TrainingTime)
                                              .ThenInclude(t => t!.Group)
                                              .Include(c => c.Childern)
                                              .FirstOrDefaultAsync
                                                   ( x =>
                                                     x.Childern!.FirstName == request.ChildName &&
                                                     x.TrainingTime!.IsTrainningTime == true &&
                                                     x.TrainingTime.Date.DayOfWeek == request.Data.DayOfWeek
                                                   );
                                            
            if ( attendenceChild == null ) 
            {
                throw new NotFoundException();
            }

            var attendenceList = new List<AttendenceListViewModel>()
            {
                new AttendenceListViewModel()
                {
                    ChildernId = attendenceChild.ChildernId,
                    Participated = attendenceChild.Participated
                }
            };

            return new AttendenceViewModel()
            {
                TraningTimeId = attendenceChild.TrainingTimeId,
                GroupName = request.ChildName,
                Tuday = attendenceChild.TrainingTime!.Date,
                TeacherId = attendenceChild.TrainingTime.Group!.TeacherId,
                AttendenceChild = attendenceList
            };
        }
    }
}
