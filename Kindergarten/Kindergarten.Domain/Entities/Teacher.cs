namespace Kindergarten.Domain.Entities
{
    public class Teacher
    {
        public Teacher() 
        {
          Groups = new HashSet<Group>();
        }
        public int Id { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set;}

        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public DateTime Bithdate { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActiveTeacher { get; set; }

        public int UserId { get; set; }


        public User? User { get; set; }

        public ICollection<Group> Groups { get; set; }

    }
}
