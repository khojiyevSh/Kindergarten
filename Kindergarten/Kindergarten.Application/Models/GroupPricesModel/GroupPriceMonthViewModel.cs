using Kindergarten.Domain.Enums;

namespace Kindergarten.Application.Models.GroupPricesModel
{
    public class GroupPriceMonthViewModel
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public bool IsActive { get; set; }

        public decimal Price { get; set; }

        public CategotyGroup CategotyGroup { get; set; }

        public AgeStatus AgeStatus { get; set; }

        public DateTime Monthdate { get; set; }
    }
}
