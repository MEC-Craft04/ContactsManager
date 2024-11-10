using ContactsManager.BL.Services.Interfaces;
using ContactsManager.DAL.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace ContactsManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ODataController
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(ODataQueryOptions<Contact> filter)
        {
            var contacts = await _contactService.GetAllContacts(filter);
            return Ok(contacts);
        }

        [HttpGet("{id:int:min(1):required}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var contact = await _contactService.GetContact(id);
                return Ok(contact);
            } 
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Contact contact)
        {
            await _contactService.CreateContact(contact);
            return Created();
        }

        [HttpPatch("{id:int:min(1):required}")]
        public async Task<IActionResult> Patch([FromRoute] int id, JsonPatchDocument<Contact> patch)
        {
            try
            {
                await _contactService.UpdateContact(id, patch);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int:min(1):required}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
