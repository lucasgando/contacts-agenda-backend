using System.ComponentModel.DataAnnotations;

namespace ContactsAgenda.Data.Models.Dtos
{
    public class UserForDeletion
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
