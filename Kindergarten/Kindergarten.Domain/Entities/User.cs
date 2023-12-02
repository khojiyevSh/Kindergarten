using Kindergarten.Domain.Enums;

namespace Kindergarten.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? PasswordHash { get; set;}

        public string? Email { get; set;}

        public bool IsActiveUser { get; set; }

        public Roles Roles { get; set; }


        public Teacher? Teacher { get; set; }

        public Childern? Childern { get; set; }
    }
}
