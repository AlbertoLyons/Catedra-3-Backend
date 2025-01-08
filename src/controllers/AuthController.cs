using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catedra_3_Backend.src.dtos;
using Catedra_3_Backend.src.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catedra_3_Backend.src.controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var auth = await _authRepository.RegisterUserAsync(registerDTO);
                return Ok(auth);
            }
            catch (Exception e)
            {
                if (e.Message == "Email already exists")
                {
                    return BadRequest(new { message = e.Message });
                }
                return StatusCode(500, new { message = e.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var auth = await _authRepository.LoginUserAsync(loginDTO);
                return Ok(auth);
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid email or password")
                {
                    return Unauthorized(new { message = e.Message });
                }
                return StatusCode(500, new { message = e.Message });
            }
        }
    }
}