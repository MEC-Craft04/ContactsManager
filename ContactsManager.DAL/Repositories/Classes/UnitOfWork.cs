using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactsManager.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public IContactRepository ContactRepository { get; init; }
        public IPhoneNumberRepository PhoneNumberRepository { get; init; }
        public IEmailRepository EmailRepository { get; init; }
        public ITypologyRepository TypologyRepository { get; init; }

        private readonly ContactsDbContext _ctx;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ContactsDbContext ctx)
        {
            _ctx = ctx;

            ContactRepository = new ContactRepository(ctx);
            PhoneNumberRepository = new PhoneNumberRepository(ctx); 
            EmailRepository = new EmailRepository(ctx);
            TypologyRepository = new TypologyRepository(ctx);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _ctx.Database.BeginTransactionAsync();
        }

        public async Task<bool> CommitAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No transaction opened");

            try
            {
                await _ctx.SaveChangesAsync();

                await _transaction.CommitAsync();
                return true;
            }
            catch
            {
                await RollBackAsync();
                return false;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollBackAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No transaction opened");

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();

            _transaction = null;
        }

        public bool IsTransactionOpen() => _transaction != null;
    }
}
