using connect_.Dto;
using connect_.Models;
using connect_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesServices _rolesServices;

        public RolesController(IRolesServices rolesServices)
        {
            _rolesServices = rolesServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var role = await _rolesServices.GetAll();
            return Ok(role);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RolesDto dto)
        {
            var role = new Role { Name = dto.Name };
            await _rolesServices.Add(role);
            return Ok(role);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] RolesDto dto)
        {
            var role = await _rolesServices.GetById(id);
            if (role == null)
                return NotFound($"no Roles was found with : {id}");
            role.Name = dto.Name;
            _rolesServices.Update(role);
            return Ok(role);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var role = await _rolesServices.GetById(id);
            if (role == null)
                return NotFound($"no Roles was found with : {id}");
            _rolesServices.Delete(role);
            return Ok(role);
        }
    }
}
