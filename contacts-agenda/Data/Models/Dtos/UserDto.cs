using ContactsAgenda.Data.Models.Enums;

namespace ContactsAgenda.Data.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
