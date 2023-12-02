using Kindergarten.Application.Abstractions;
using Kindergarten.Application.Exceptions;
using Kindergarten.Application.Models.GroupModels;
using Kindergarten.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kindergarten.Application.UseCase.Admins.Commands.GroupCommands
{
    public class UpdateGroupCommand :ICommand<GroupViewModel>
    {
       
        public string? OldName { get; set; }

        public string? NewName { get; set; }

        public int MaxChildCount { get; set; }

        public DateTime StartData { get; set; }

        public DateTime EndData { get; set; }
    }

    public class UpdateGroupCommandHandler : ICommandHandler<UpdateGroupCommand, GroupViewModel>
    {
        private readonly IApplicationDbContext _context;

        public UpdateGroupCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroupViewModel> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Name == request.OldName);
            var groupPrices =  _context.GroupPrices.Where(x=>x.GroupId == group.Id).ToHashSet();
            var trainingTimes =  _context.TrainingTimes.Where(x=>x.GroupId == group.Id).ToHashSet();


            if (group == null && groupPrices == null && trainingTimes == null)
            {
                throw new NotFoundException();
            }

            group!.StartData = request.StartData;
            group.EndData = request.EndData;
            group.IsActive = true;
            group.Name = request.NewName ?? group.Name;

              _context.Groups.Update(group);
              
              _context.GroupPrices.RemoveRange(groupPrices!);
   
            dynamic monthCount;

            if (group.StartData.Year != group.EndData.Year)
            {
                monthCount = (group.EndData.Month + 12) - group.StartData.Month;
            }
            else { monthCount = (group.EndData.Month - group.StartData.Month); }

            var groupPriceList = new List<GroupPrice>();

            var monthData = group.StartData;

            decimal price =0;
            dynamic ageStatus=0;
            dynamic categoryGroup=0;

            foreach (var groupPrice in groupPrices!)
            {
                price = groupPrice.Price;
                ageStatus = groupPrice.AgeStatus;
                categoryGroup = groupPrice.CategotyGroup;
                break;
            }

            for (int i = 0;i<monthCount;i++)
            {
                monthData = monthData.AddMonths(1);

                groupPriceList.Add(new GroupPrice()
                {
                    Price = price,
                    AgeStatus = ageStatus,
                    CategotyGroup = categoryGroup,
                    GroupId = group.Id,
                    Monthdate = monthData,
                    IsActive = true
                });

                if (monthData == request.EndData) { break; }
                
                if (monthData.AddMonths(1) > request.EndData )
                {
                    groupPriceList.Add(new GroupPrice()
                    {
                        Price = price,
                        AgeStatus = ageStatus,
                        CategotyGroup = categoryGroup,
                        GroupId = group.Id,
                        Monthdate = request.EndData,
                        IsActive=true
                    });
                    break;
                }
            }

           await _context.GroupPrices.AddRangeAsync(groupPriceList);

            var totalday = (group.EndData.Date - group.StartData.Date).Days;

            var trainningTimeList = new List<TrainingTime>();

            var currentDay = group.StartData;

            var starTime = TimeSpan.MinValue.ToString();
            var endTime = TimeSpan.MaxValue.ToString();

            foreach (var trainingTime in trainingTimes)
            {
                if (!trainingTime.IsTrainningTime)
                {
                    _context.TrainingTimes.Remove(trainingTime);
                }

                if(starTime.ToString() != (trainingTime.StartTime.ToString("HH:mm:ss")))
                {
                    starTime = trainingTime.StartTime.ToString("HH:mm:ss");
                    endTime = trainingTime.EndTime.ToString("HH:mm:ss");
                }
            } 

            for (var i = 1; i <= totalday;i++)
            {
                if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
                {
                    trainningTimeList.Add(new TrainingTime()
                    {
                        Date = currentDay,
                        StartTime = Convert.ToDateTime(currentDay.ToString("yyyy-MM-dd ") + starTime),
                        EndTime = Convert.ToDateTime(currentDay.ToString("yyyy-MM-dd ") + endTime),
                        GroupId = group.Id
                    });
                }
                currentDay = currentDay.AddDays(1);
            }

            await _context.TrainingTimes!.AddRangeAsync(trainningTimeList);
            await _context.SaveChangesAsync(cancellationToken);

            return new GroupViewModel()
            {
                AgeStatus = ageStatus,
                CategotyGroup = categoryGroup,
                GroupPrice = price,
                LessonStartTime = Convert.ToDateTime(starTime),
                LessonEndTime = Convert.ToDateTime(endTime),
                Date = DateTime.Now,
                EndData = group.EndData,
                StartData = group.StartData,
                GroupId = group.Id,
                Name = group.Name,
                TeacherId = group.TeacherId,
                MaxChildCount =group.MaxChildCount
            };
        }
    }
}
