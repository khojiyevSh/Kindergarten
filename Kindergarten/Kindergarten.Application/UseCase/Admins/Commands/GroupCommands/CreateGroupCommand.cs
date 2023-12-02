using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Models.GroupModels;
using Kindergarten.Domain.Entities;
using Kindergarten.Domain.Enums;

namespace Kindergarten.Application.UseCase.Admins.Commands.GroupCommands
{
    public class CreateGroupCommand : ICommand<GroupViewModel>
    {
        public string? Name { get; set; }

        public int TeacherId { get; set; }

        public int MaxChildCount { get; set; }

        public DateTime StartData { get; set; }

        public DateTime EndData { get; set; }

        public decimal Price { get; set; }

        public CategotyGroup CategotyGroup { get; set; }

        public AgeStatus AgeStatus { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan LessonStartTime { get; set; }

        public TimeSpan LessonEndTime { get; set; }
    }

    public class CreateGroupCommandHandler : ICommandHandler<CreateGroupCommand, GroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public CreateGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroupViewModel> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group()
            {
                Name = request.Name,
                TeacherId = request.TeacherId,
                MaxChildCount = request.MaxChildCount,
                StartData = request.StartData,
                EndData = request.EndData,
                IsActive = true
            };

            if (request.StartData > request.EndData)
            {
                throw new Exception();
            }

            await _context.Groups!.AddAsync(group);
            await _context.SaveChangesAsync(cancellationToken);

            dynamic month;

            if (group.StartData.Year != group.EndData.Year)
            {
                month = (group.EndData.Month + 12) - group.StartData.Month;
            }
            else { month = (group.EndData.Month - group.StartData.Month); }

            var groupPriceList = new List<GroupPrice>();

            var months = group.StartData;

            for (int i = 0; i < month; i++)
            {
                months = months.AddMonths(1);
                
                groupPriceList.Add(new GroupPrice()
                {
                    Price = request.Price,
                    AgeStatus = request.AgeStatus,
                    CategotyGroup = request.CategotyGroup,
                    GroupId = group.Id,
                    Monthdate = months,
                    IsActive = true
                });
               
                if (months == request.EndData) { break; }

                if (months.AddMonths(1) > request.EndData)
                {
                    groupPriceList.Add(new GroupPrice()
                    {
                        Price = request.Price,
                        AgeStatus = request.AgeStatus,
                        CategotyGroup = request.CategotyGroup,
                        GroupId = group.Id,
                        Monthdate = request.EndData,
                        IsActive = true
                    });
                    break;
                }
            }

            await _context.GroupPrices!.AddRangeAsync(groupPriceList);
            await _context.SaveChangesAsync(cancellationToken);

            var totalday = (group.EndData.Date - group.StartData.Date).Days;

            var trainningTimeList = new List<TrainingTime>();

            var currentDay = group.StartData;
            for (int i = 1; i <= totalday; i++)
            {
                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    trainningTimeList.Add(new TrainingTime()
                    {
                        Date = currentDay,
                        StartTime = Convert.ToDateTime(currentDay + request.LessonStartTime),
                        EndTime = Convert.ToDateTime(currentDay + request.LessonEndTime),
                        GroupId = group.Id
                    });
                }
                currentDay = currentDay.AddDays(1);
            }

            await _context.TrainingTimes!.AddRangeAsync(trainningTimeList);
            await _context.SaveChangesAsync(cancellationToken);

            return new GroupViewModel()
            {
                AgeStatus = request.AgeStatus,
                CategotyGroup = request.CategotyGroup,
                EndData = request.EndData,
                StartData = request.StartData,
                Date = DateTime.Now,
                GroupId = group.Id,
                LessonEndTime = Convert.ToDateTime(currentDay + request.LessonEndTime),
                LessonStartTime = Convert.ToDateTime(currentDay + request.LessonStartTime),
                MaxChildCount = request.MaxChildCount,
                Name = request.Name,
                GroupPrice= request.Price,
            };
        }
    }
}
