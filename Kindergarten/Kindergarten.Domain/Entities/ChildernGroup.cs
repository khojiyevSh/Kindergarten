namespace Kindergarten.Domain.Entities
{
    public class ChildernGroup
    {
        public int Id { get; set; } 

        public bool IsActive { get; set; }

        public bool IsPayed { get; set; }

        public int ChildernId { get; set; }

        public int GroupId { get; set; }


        public Childern? Childern { get; set; }

        public Group? Group { get; set; }

    }
}
