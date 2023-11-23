using ContactsAgenda.Data.Entities;
using ContactsAgenda.Data.Models.Enums;
using ContactsAgenda.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ContactsAgenda.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = 1,
                        Username = "admin",
                        Email = "admin@mail.com",
                        PasswordHash = PasswordHashing.GetPasswordHash("admin"),
                        Role = UserRoleEnum.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Username = "user",
                        Email = "user@mail.com",
                        PasswordHash = PasswordHashing.GetPasswordHash("password"),
                        Role = UserRoleEnum.User
                    }
                );
            modelBuilder.Entity<Contact>()
                .HasData(
                    new Contact()
                    {
                        Id = 1,
                        Name = "contact1",
                        LastName = "c2",
                        Address = "",
                        Email = "contact1@mail.com",
                        PhoneNumber = "1",
                        Description = "contact1",
                        ProfilePicture = "",
                        UserId = 1
                    },
                    new Contact()
                    {
                        Id = 2,
                        Name = "contact2",
                        LastName = "c2",
                        Address = "la paz",
                        Email = "contact2@mail.com",
                        PhoneNumber = "2",
                        Description = "contact2",
                        ProfilePicture = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fblog.confirmbets.com%2Fwp-content%2Fuploads%2F2019%2F07%2FMessi.jpg&f=1&nofb=1&ipt=6035a6696880fc216b7b9936ccdd11f3598497d322ae62f73fc7ae390179b0ca&ipo=images",
                        UserId = 2
                    }
                );

            modelBuilder.Entity<User>(user =>
            {
                user.HasMany(u => u.Contacts)
                .WithOne(c => c.User);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}

// on package manager console:
// Add-Migration <migration-name> -Context <context-name>

// update-database