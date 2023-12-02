namespace Kindergarten.Domain.Entities
{
    public class Attendence
    {
        public int Id { get; set; }
      
        public bool Participated { get; set; }
        
        public int ChildernId { get; set; }

        public int TrainingTimeId { get; set; }


        public Childern? Childern { get; set; }

        public TrainingTime? TrainingTime { get; set; }
    }
}
