using ContactsManager.BL.Services.Interfaces;
using ContactsManager.DAL.Data;
using ContactsManager.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace ContactsManager.BL.Services.Classes
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        { 
            _unitOfWork = unitOfWork;
        }

        public Task<List<Contact>> GetAllContacts(ODataQueryOptions<Contact> filter)
        {
            IQueryable contacts = _unitOfWork.ContactRepository.GetAll();
            contacts = filter.ApplyTo(contacts);
            
            return contacts
                .Cast<Contact>()
                .ToListAsync();
        }

        public async Task<Contact> GetContact(int id)
        {
            Contact? contact = await _unitOfWork.ContactRepository.GetById(id);

            if (contact == null)
            {
                if (_unitOfWork.IsTransactionOpen()) 
                    await _unitOfWork.RollBackAsync();

                throw new KeyNotFoundException("Element not found");
            }

            return contact;
        }

        public async Task<Contact> CreateContact(Contact contact)
        {
            await _unitOfWork.BeginTransactionAsync();

            Contact created = await _unitOfWork.ContactRepository.Create(contact);
            await _unitOfWork.CommitAsync();

            return created;
        }

        public async Task<Contact> UpdateContact(int id, JsonPatchDocument<Contact> patch)
        {
            await _unitOfWork.BeginTransactionAsync();

            Contact toUpdate = await GetContact(id);
            patch.ApplyTo(toUpdate);

            await _unitOfWork.CommitAsync();
            return toUpdate;
        }

        public async Task DeleteContact(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            bool deleted = await _unitOfWork.ContactRepository.Delete(id);

            if (!deleted)
            {
                await _unitOfWork.RollBackAsync();
                throw new KeyNotFoundException("Element not found");
            }

            await _unitOfWork.CommitAsync();
        }
    }
}
