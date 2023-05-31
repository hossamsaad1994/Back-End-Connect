using Connect.Services;
using connect_.Dto;
using connect_.Models;
using connect_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace connect_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppusersController : ControllerBase
    {
        private readonly IAppuserServices _Appuserservices;

        public AppusersController(IAppuserServices services)
        {
            _Appuserservices = services;
        }
        [HttpGet]
        public async Task<IActionResult> GetallAsync()
        {
            var appusers = await _Appuserservices.GetAll();
            return Ok(appusers);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AppuserDto dto)
        {
            var appuser = new Appuser
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                RoleID = dto.RoleID,
                IsActive = dto.IsActive,
            };
            await _Appuserservices.Add(appuser);
            return Ok(appuser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] AppuserDto dto)
        {
            var appuser = await _Appuserservices.GetById(id);
            if (appuser == null)
                return NotFound($"no app user was found with : {id}");
            appuser.Username = dto.Username;
            _Appuserservices.Update(appuser);
            return Ok(appuser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var appuser = await _Appuserservices.GetById(id);
            if (appuser == null)
                return NotFound($"no app user was found with : {id}");
            _Appuserservices.Delete(appuser);
            return Ok(appuser);

        }
    }
}
