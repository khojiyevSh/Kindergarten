namespace Kindergarten.Application.Models
{
    public class GetGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartData { get; set; }

        public DateTime EndData { get; set; }

        public int MaxChildCount { get; set; }

        public bool IsActive { get; set; }
    }
}
