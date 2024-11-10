namespace ContactsManager.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IContactRepository ContactRepository { get; init; }
        IPhoneNumberRepository PhoneNumberRepository { get; init; }
        IEmailRepository EmailRepository { get; init; }
        ITypologyRepository TypologyRepository { get; init; }

        Task BeginTransactionAsync();
        Task<bool> CommitAsync();
        Task RollBackAsync();
        bool IsTransactionOpen();
    }
}
