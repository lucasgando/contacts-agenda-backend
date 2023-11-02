using System.ComponentModel.DataAnnotations;

namespace ContactsAgenda.Data.Models.Dtos
{
    public class ContactForUpdate
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Description = string.Empty;
    }
}
