using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.DAL.Repositories.Classes
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ContactsDbContext ctx) : base(ctx) { }

        public override Task<Contact?> GetById(int id)
        {
            return _dbSet
                    .Include(c => c.PhoneNumbers)!
                    .ThenInclude(p => p.Typology)
                    .Include(c => c.Emails)!
                    .ThenInclude(e => e.Typology)
                    .SingleOrDefaultAsync(c => c.ContactId == id);
        }

        public override async Task<bool> Delete(int id)
        {
            Contact? toDelete = await base.GetById(id);

            if (toDelete == null)
                return false;

            toDelete.IsDeleted = true;
            return true;
        }
    }
}
