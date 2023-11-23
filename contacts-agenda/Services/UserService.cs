using ContactsAgenda.Data;
using ContactsAgenda.Data.Entities;
using ContactsAgenda.Data.Models.Dtos;
using ContactsAgenda.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ContactsAgenda.Services
{
    public class UserService
    {
        private readonly AgendaContext _context;
        public UserService(AgendaContext context)
        {
            _context = context;
        }
        public IEnumerable<UserDto> GetAll()
        {
            return _context.Users.Select(u => new UserDto()
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                Role = u.Role
            }).ToList();
        }
        public UserDto? GetById(int id)
        {
            User? user = _context.Users.SingleOrDefault(user => user.Id == id);
            return user is null ? null : new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Role = user.Role
            };
        }
        public UserDto? GetByEmail(string email)
        {
            User? user = _context.Users.SingleOrDefault(user => user.Email == email.ToLower());
            return user is null ? null : new UserDto()
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                Role = user.Role
            };
        }
        public bool Authenticate(string email, string password)
        {
            UserDto? user = GetByEmail(email);
            if (user == null) return false;
            return user.PasswordHash == PasswordHashing.GetPasswordHash(password);
        }
        public bool Exists(string email)
        {
            return GetByEmail(email.ToLower()) is not null;
        }
        public void Add(UserForCreation dto)
        {
            User newUser = new User()
            {
                Username = dto.Username,
                Email = dto.Email.ToLower(),
                PasswordHash = PasswordHashing.GetPasswordHash(dto.Password)
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
        public void Update(UserForUpdate dto)
        {
            User user = _context.Users.Single(u => u.Email == dto.Email.ToLower());
            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = PasswordHashing.GetPasswordHash(dto.Password);
            _context.SaveChanges();
        }
        public void Delete(UserForDeletion dto)
        {
            User userToDelete = _context.Users.Include(x => x.Contacts).Single(u => u.Email == dto.Email);
            foreach (Contact contact in userToDelete.Contacts)
            {
                _context.Contacts.Remove(contact);
            }
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }
}
