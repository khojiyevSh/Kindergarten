namespace Kindergarten.Domain.Entities
{
    public class TrainingTime
    {
        public TrainingTime() 
        {
          Attendences = new HashSet<Attendence>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int GroupId { get; set; }


        public Group? Group { get; set; }

        public ICollection<Attendence> Attendences { get; set; }
    }
}
