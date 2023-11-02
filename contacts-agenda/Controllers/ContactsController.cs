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
    public class ContactsController : Controller
    {
        private readonly ContactService _service;
        public ContactsController(ContactService contactService)
        {
            _service = contactService;
        }
        [HttpGet("admin/contacts")]
        public IActionResult GetAll()
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole is not "Admin") return Forbid();
            return Ok(_service.GetAll().ToList());
        }
        [HttpGet("admin/contacts/{id}")]
        public IActionResult GetById(int id)
        {
            string userRole = User.Claims.First(claim => claim.Type.Contains("role")).Value;
            if (userRole is not "Admin") return Forbid();
            ContactDto? contact = _service.GetById(id);
            if (contact != null)
            {
                return Ok(contact);
            }
            return NotFound("Contact not found");
        }
        [HttpGet("contacts")]
        public IActionResult GetUserContacts()
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            List<ContactDto>? contacts = _service.GetByUserId(userId);
            return Ok(contacts);
        }
        [HttpPost]
        public IActionResult Create(ContactForCreation dto)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            int id = _service.Add(dto, userId);
            return Ok(id);
        }
        [HttpPut]
        public IActionResult Update(ContactForUpdate dto)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            ContactDto? contact = _service.GetById(dto.Id);
            if (contact is null) return NotFound();
            if (userId != contact.UserId) return Forbid();
            bool res = _service.Update(dto);
            return res ? Ok() : BadRequest();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            int userId = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier"))!.Value);
            ContactDto? contact = _service.GetById(id);
            if (contact is null) return BadRequest();
            if (userId != contact.UserId) return Forbid();
            bool res = _service.Delete(id);
            return res ? Ok() : BadRequest();
        }
    }
}
