using ContactsAgenda.Data.Entities;
using ContactsAgenda.Data.Models.Enums;
using ContactsAgenda.Helpers;
using Microsoft.AspNetCore.Identity;
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
                        Email = "contact1@mail.com",
                        PhoneNumber = "1",
                        Description = "contact1",
                        UserId = 1
                    },
                    new Contact()
                    {
                        Id = 2,
                        Name = "contact2",
                        LastName = "c2",
                        Email = "contact2@mail.com",
                        PhoneNumber = "2",
                        Description = "contact2",
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