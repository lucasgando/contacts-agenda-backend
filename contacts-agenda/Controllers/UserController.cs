using contacts_agenda.Controllers;
using ContactsAgenda.Data.Models.Dtos;
using ContactsAgenda.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }
        [HttpGet("admin/users")]
        public IActionResult GetAll()
        {
            if (!Admin()) return Forbid();
            return Ok(_service.GetAll());
        }
        [HttpGet("admin/users/{id}")]
        public IActionResult GetById(int id)
        {
            if (!Admin()) return Forbid();
            UserDto? user = _service.GetById(id);
            if (user is not null) return Ok(user);
            return NotFound("User not found");
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Create([FromBody] UserForCreation dto)
        {
            if (_service.Exists(dto.Email)) return Conflict("Email taken");
            _service.Add(dto);
            UserDto newUser = _service.GetByEmail(dto.Email)!;
            return Created(newUser.Id.ToString(), newUser);
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserForUpdate dto)
        {
            if (!Admin() && Email() != dto.Email) return Forbid();
            _service.Update(dto);
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete([FromBody] UserForDeletion dto)
        {
            if (!Admin() && Email() != dto.Email) return Forbid();
            if (!_service.Exists(dto.Email)) return NotFound("User not found");
            _service.Delete(dto);
            return NoContent();
        }
    }
}
