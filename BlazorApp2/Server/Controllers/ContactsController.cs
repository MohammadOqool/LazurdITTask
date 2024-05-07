using BlazorApp2.Server.Controllers.Base;
using BlazorApp2.Server.Interfaces;
using BlazorApp2.Server.Repositories;
using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : BaseController
    {
        private readonly ContactsRepository _contactsRepository;

        public ContactsController(IGenericRepository<Contact> contactsRepository)
        {
            this._contactsRepository = (ContactsRepository)contactsRepository;
        }


        [HttpGet, Route("getAllContacts")]
        public IActionResult GetAllContacts()
        {
            try
            {
                return HandleResponse(_contactsRepository.GetAll());

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet, Route("getContactById")]
        public IActionResult GetContactById(int id)
        {
            try
            {
                return HandleResponse(_contactsRepository.Get(id));

            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost, Route("addContact")]
        public IActionResult AddContact(Contact contact)
        {
            try
            {
                if (!IsValidParams())
                    return OnMissingParameters();

                var newContact = _contactsRepository.Add(contact);
                if (newContact == null)
                    return HandleResponse(errorMessage: "Record already exist");

                return HandleResponse(newContact);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpGet(), Route("deleteContact")]
        public IActionResult DeleteContact(int id)
        {
            if (!_contactsRepository.IsExist(id))
            {
                return HandleResponse(errorMessage: "No record found");
            }

            _contactsRepository.Remove(id);
            return HandleResponse(true);
        }

        [HttpPost, Route("updateContact/{id}")]
        public IActionResult UpdateContact(int id, Contact contact)
        {
            try
            {
                if (!IsValidParams())
                    return OnMissingParameters();

                var existingContact = _contactsRepository.Get(id);
                if (existingContact == null)
                    return HandleResponse(errorMessage: "No Record found");

                var updatedContact = _contactsRepository.Update(id, contact);
                return HandleResponse(updatedContact);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
