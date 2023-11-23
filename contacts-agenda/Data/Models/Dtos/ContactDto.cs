namespace contacts_agenda.Data.Models.Dtos
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Description = string.Empty;
        public string ProfilePicture { get; set; }
    }
}
