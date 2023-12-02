using Kindergarten.Domain.Entities;

namespace Kindergarten.Application.Models.Attendences
{
    public class AttendenceViewModel
    {
        public int TraningTimeId { get; set; }

        public string? GroupName { get; set; }

        public int TeacherId { get; set; }

        public DateTime Tuday { get; set; }

        public  List<AttendenceListViewModel>? AttendenceChild { get; set; }
    }
}
