using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;

namespace ContactsManager.DAL.Repositories.Classes
{
    public class PhoneNumberRepository : GenericRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(ContactsDbContext ctx) : base(ctx) { }

        public override async Task<bool> Delete(int id)
        {
            PhoneNumber? toDelete = await base.GetById(id);

            if (toDelete == null)
                return false;

            toDelete.IsDeleted = true;
            return true;
        }
    }
}
