using System.ComponentModel.DataAnnotations;

namespace ContactsAgenda.Data.Models.Dtos
{
    public class ContactForCreation
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ProfilePicture { get; set; }
    }
}
