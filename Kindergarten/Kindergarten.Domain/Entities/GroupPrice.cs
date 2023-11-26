using Kindergarten.Domain.Enums;

namespace Kindergarten.Domain.Entities
{
    public class GroupPrice
    {
        public int Id { get; set; }

        public DateTime Monthdate { get; set; }

        public bool IsActive { get; set; }

        public decimal Price { get; set; }

        public CategotyGroup CategotyGroup { get; set; }

        public AgeStatus AgeStatus { get; set; }

        public int GroupId { get; set; }


        public Group? Group { get; set; }

    }
}
