using Kindergarten.Domain.Enums;

namespace Kindergarten.Application.Models.GroupModels
{
    public class GroupViewModel
    {
        public string? Name { get; set; }

        public int GroupId { get; set; }

        public decimal GroupPrice { get; set; }

        public int TeacherId { get; set; }

        public int MaxChildCount { get; set; }

        public DateTime StartData { get; set; }

        public DateTime EndData { get; set; }

        public CategotyGroup CategotyGroup { get; set; }

        public AgeStatus AgeStatus { get; set; }

        public DateTime Date { get; set; }

        public DateTime LessonStartTime { get; set; }

        public DateTime LessonEndTime { get; set; }
    }
}
