using System.ComponentModel.DataAnnotations;

namespace ContactsAgenda.Data.Models.Dtos
{
    public class CredentialsDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
