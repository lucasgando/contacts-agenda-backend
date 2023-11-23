using contacts_agenda.Controllers;
using contacts_agenda.Data.Models.Dtos;
using ContactsAgenda.Data.Models.Dtos;
using ContactsAgenda.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAgenda.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactsController : BaseController
    {
        private readonly ContactService _service;
        public ContactsController(ContactService contactService)
        {
            _service = contactService;
        }
        [HttpGet("admin/contacts")]
        public IActionResult GetAll()
        {
            if (!Admin()) return Forbid();
            return Ok(_service.GetAll().ToList());
        }
        [HttpGet("admin/contacts/{id}")]
        public IActionResult GetById(int id)
        {
            if (!Admin()) return Forbid();
            ContactDto? contact = _service.GetById(id);
            if (contact is not null) return Ok(contact);
            return NotFound("Contact not found");
        }
        [HttpGet("contacts")]
        public IActionResult GetUserContacts()
        {
            return Ok(_service.GetByUserId(UserId()));
        }
        [HttpGet("contacts/{id}")]
        public IActionResult GetUserContact(int id)
        {
            if (_service.UserOwnsContact(UserId(), id)) return Ok(_service.GetById(id));
            return Forbid();
        }
        [HttpPost]
        public IActionResult Create(ContactForCreation dto)
        {
            int id = _service.Add(dto, UserId());
            return Created("", id);
        }
        [HttpPut]
        public IActionResult Update(ContactForUpdate dto)
        {
            ContactDto? contact = _service.GetById(dto.Id);
            if (contact is null) return NotFound();
            if (!_service.UserOwnsContact(UserId(), contact.Id)) return Forbid();
            bool res = _service.Update(dto);
            return res ? Ok() : BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ContactDto? contact = _service.GetById(id);
            if (contact is null) return BadRequest();
            if (!_service.UserOwnsContact(UserId(), contact.Id)) return Forbid();
            bool res = _service.Delete(id);
            return res ? Ok() : BadRequest();
        }
    }
}
