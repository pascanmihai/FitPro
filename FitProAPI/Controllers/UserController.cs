using FitProDB.Models;
using FitProDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FitProAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    //Authorize

    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("Get-User")]
        public async Task<IActionResult> Get(long id)
        {
            var entity = await _userRepository.GetUserById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                if (user == null)
                    return BadRequest();
                var created = await _userRepository.Register(user);
                if (created == null)
                    return Problem();
                return Ok(created);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var access = await _userRepository.Login(username);
            if (access == null)
                return Unauthorized("incorrect username");
            var isPasswordValid = await _userRepository.CheckPassword(password);
            if (isPasswordValid == null)
                return Unauthorized("invalid password");
            return Ok("login succesfully");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(long id, User user)
        {
            if(id != user.Id)
                return BadRequest();

            var entity = await _userRepository.Update(user);
            if (entity == null)
                return BadRequest();
            return Ok(entity);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                await _userRepository.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error deleting data");
            }
        }
    }
}
