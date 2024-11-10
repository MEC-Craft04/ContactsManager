using ContactsManager.DAL.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;

namespace ContactsManager.BL.Services.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllContacts(ODataQueryOptions<Contact> filter);
        Task<Contact> GetContact(int id);
        Task<Contact> CreateContact(Contact contact);
        Task<Contact> UpdateContact(int id, JsonPatchDocument<Contact> patch);
        Task DeleteContact(int id);
    }
}
