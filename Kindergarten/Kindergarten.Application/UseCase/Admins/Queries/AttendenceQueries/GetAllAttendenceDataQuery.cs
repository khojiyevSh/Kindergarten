using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.Attendences;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Queries.AttendenceQueries
{
    public class GetAllAttendenceDataQuery : IQuery<AttendenceViewModel>
    {
        public DateTime Date { get; set; }

        public int TrainingTimeId { get; set; }
    }

    public class GetAllAttendenceDataQueryHandler : IQueryHandler<GetAllAttendenceDataQuery, AttendenceViewModel>
    {
        private readonly IApplicationDbContext _context;

        public GetAllAttendenceDataQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AttendenceViewModel> Handle(GetAllAttendenceDataQuery request, CancellationToken cancellationToken)
        {
            var attendenceChilds = await _context.Attendences
                                                    .Include(t=>t.TrainingTime)
                                                    .ThenInclude(g=>g!.Group)
                                                    .Where(x=>x.TrainingTime!.Date.DayOfWeek == request.Date.DayOfWeek &&
                                                              x.TrainingTimeId == request.TrainingTimeId)
                                                    .ToListAsync(cancellationToken);

            if (attendenceChilds.Count == 0)
            {
                throw new NotFoundException();
            }

            var attendenceList = new List<AttendenceListViewModel>();

            var attendenceViewModel = new AttendenceViewModel();

            foreach (var child in attendenceChilds)
            {
               if (attendenceViewModel.GroupName == null)
                {
                    attendenceViewModel!.TeacherId = child.TrainingTime!.Group!.TeacherId;
                    attendenceViewModel.TraningTimeId = child.TrainingTimeId;
                    attendenceViewModel.Tuday = child.TrainingTime.Date;
                    attendenceViewModel.GroupName = child.TrainingTime.Group.Name;
                }

                attendenceList.Add(new AttendenceListViewModel
                {
                    ChildernId = child.ChildernId,
                    Participated = child.Participated,
                });
            }

            attendenceViewModel.AttendenceChild = attendenceList;

            return attendenceViewModel;
        }
    }
}
