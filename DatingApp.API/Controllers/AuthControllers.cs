using System;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto pUserForRegisterDto)
        {
            string userName = pUserForRegisterDto.userName.ToLower();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _repo.UserExits(userName))
            {
                return BadRequest("ya existe el usuario");
            }

            var userToCreate = new Users
            {
                UserName = userName
            };
            var createduser = _repo.Register(userToCreate, pUserForRegisterDto.password);

            return StatusCode(201);
        }
    }
}