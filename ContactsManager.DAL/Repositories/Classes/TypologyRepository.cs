using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;

namespace ContactsManager.DAL.Repositories.Classes
{
    public class TypologyRepository : GenericRepository<Typology>, ITypologyRepository
    {
        public TypologyRepository(ContactsDbContext ctx) : base(ctx) { }

        public override async Task<bool> Delete(int id)
        {
            Typology? toDelete = await base.GetById(id);

            if (toDelete == null)
                return false;

            toDelete.IsDeleted = true;
            return true;
        }
    }
}
