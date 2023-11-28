using Kindergarten.Domain.Enums;

namespace Kindergarten.Application.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? MiddleName { get; set; }

        public string? Email { get; set; }

        public DateTime Bithdate { get; set; }

        public string? PhoneNumber { get; set; }

        public int UserId { get; set; }

        public Roles Roles { get; set; }
    }
}
