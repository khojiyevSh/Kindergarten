namespace Kindergarten.Domain.Entities
{
    public class Group
    {
        public Group() 
        {
            GroupPrices = new HashSet<GroupPrice>();
            TrainingTimes = new HashSet<TrainingTime>();
            ChildernGroups = new HashSet<ChildernGroup>();
        }
        public int Id { get; set; }

        public string? Name { get; set; }

        public int MaxChildCount { get; set; }

        public DateTime StartData { get; set; }

        public DateTime EndData { get; set; }

        public int TeacherId { get; set; }


        public Teacher? Teacher { get; set; }

        public ICollection<GroupPrice> GroupPrices { get; set; }

        public ICollection<TrainingTime> TrainingTimes { get; set; }

        public ICollection<ChildernGroup> ChildernGroups { get; set; }

    }
}
