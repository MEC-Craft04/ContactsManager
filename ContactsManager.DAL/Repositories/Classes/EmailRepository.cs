using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;

namespace ContactsManager.DAL.Repositories.Classes
{
    public class EmailRepository : GenericRepository<Email>, IEmailRepository
    {
        public EmailRepository(ContactsDbContext ctx) : base(ctx) { }

        public override async Task<bool> Delete(int id)
        {
            Email? toDelete = await base.GetById(id);

            if (toDelete == null)
                return false;

            toDelete.IsDeleted = true;
            return true;
        }
    }
}
