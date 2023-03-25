using M1PROJECTAPI_YAO_.Context;
using M1PROJECTAPI_YAO_.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M1PROJECTAPI_YAO_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;
        public UserController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserModel userObj)
        {
            if (userObj == null)
                return BadRequest();

            var user = await _userDbContext.Users.FirstOrDefaultAsync(x => x.UserName == userObj.UserName && x.Password == userObj.Password);
            if (user == null)
            {
                return NotFound(new { Message = "User Not Found! " });
            }

            return Ok(new
            {
                Message = "Login Success !"
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel userObj)
        {
            if (userObj == null)
                return BadRequest();
            await _userDbContext.Users.AddAsync(userObj);
            await _userDbContext.SaveChangesAsync();
            return Ok(
                new
                {
                    Message = "User Registered! "
                }
                ); ;
        }
    }
}
