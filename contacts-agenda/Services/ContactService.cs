using contacts_agenda.Data.Models.Dtos;
using ContactsAgenda.Data;
using ContactsAgenda.Data.Entities;
using ContactsAgenda.Data.Models.Dtos;

namespace ContactsAgenda.Services
{
    public class ContactService
    {
        private readonly AgendaContext _context;
        public ContactService(AgendaContext context)
        {
            _context = context;
        }
        public List<ContactDto> GetAll()
        {
            return _context.Contacts.Select(c => new ContactDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    LastName = c.LastName,
                    Address = c.Address,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Description = c.Description,
                    ProfilePicture = c.ProfilePicture
                }).ToList();
        }
        public ContactDto? GetById(int id)
        {
            Contact? contact = _context.Contacts.SingleOrDefault(c => c.Id == id);
            return contact is null
                ? null
                : new ContactDto()
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        LastName = contact.LastName,
                        Address = contact.Address,
                        Email = contact.Email,
                        PhoneNumber = contact.PhoneNumber,
                        Description = contact.Description,
                        ProfilePicture = contact.ProfilePicture
                    };
        }
        public List<ContactDto> GetByUserId(int userId)
        {
            return _context.Contacts.Where(c => c.UserId == userId)
                .Select(c => new ContactDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    LastName = c.LastName,
                    Address = c.Address,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Description = c.Description,
                    ProfilePicture = c.ProfilePicture
                }).ToList();
        }
        public bool UserOwnsContact(int userId, int contactId)
        {
            Contact? contact = _context.Contacts.SingleOrDefault(c => c.Id == contactId);
            if (contact is null) return false;
            return contact.UserId == userId;
        }
        public int Add(ContactForCreation dto, int userId)
        {
            Contact contact = new Contact()
            {
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                Description = dto.Description,
                ProfilePicture = dto.ProfilePicture,
                UserId = userId
            };
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact.Id;
        }
        public bool Update(ContactForUpdate dto)
        {
            Contact? contact = _context.Contacts.FirstOrDefault(c => c.Id == dto.Id);
            if (contact is null) return false;
            contact.Name = dto.Name;
            contact.LastName = dto.LastName;
            contact.Address = dto.Address;
            contact.Email = dto.Email;
            contact.PhoneNumber = dto.PhoneNumber;
            contact.Description = dto.Description;
            contact.ProfilePicture = dto.ProfilePicture;
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            Contact? contactToDelete = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contactToDelete is null) return false;
            _context.Contacts.Remove(contactToDelete);
            _context.SaveChanges();
            return true;
        }
        public void DeleteByUser(int userId)
        {
            List<Contact> contacts = _context.Contacts.Where(c => c.UserId == userId).ToList();
            foreach (Contact contact in contacts)
                _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }
    }
}
