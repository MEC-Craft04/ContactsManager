using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.DAL.Data
{
    public class ContactsDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Typology> Typologies { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Contact>()
                .HasQueryFilter(c => !c.IsDeleted);

            builder.Entity<PhoneNumber>()
                .HasQueryFilter(p => !p.IsDeleted);

            builder.Entity<Email>()
                .HasQueryFilter(e => !e.IsDeleted);

            builder.Entity<Typology>()
                .HasQueryFilter(t => !t.IsDeleted);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName!.StartsWith("AspNet"))
                    entityType.SetTableName(tableName.Substring(6));
            }
        }
    }
}
