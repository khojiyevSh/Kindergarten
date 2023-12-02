namespace Kindergarten.Application.Models.ChildGroupModels
{
    public class GetChildGroupViewModel
    {
       public int ChildGroupId { get; set; }

        public int ChildrenId { get; set; }

        public string? Username { get; set; }

        public string? FirstName { get; set; }

        public string? FatherNumber { get; set; }

        public string? MatherNumber { get; set; }

        public string? GroupName { get; set; }

        public bool? IsPayed { get; set; }
    }
}
