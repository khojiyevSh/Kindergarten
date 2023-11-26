namespace Kindergarten.Domain.Entities
{
    public class Childern
    {
        public Childern() 
        {
          Attendences = new HashSet<Attendence>();
          ChildernGroups = new HashSet<ChildernGroup>();
        }

        public int Id { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public DateTime Bithdate { get; set; }

        public string? FatherNumber { get; set; }

        public string? MatherNumber { get; set; }

        public int UserId { get; set; }


        public User? User { get; set; }

        public ICollection<Attendence> Attendences { get; set; }

        public ICollection<ChildernGroup> ChildernGroups { get; set; }

    }
}
