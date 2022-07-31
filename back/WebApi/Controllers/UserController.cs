using BLL;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        // GET: api/User
        [HttpGet]
        public async Task<List<User>> Get()
        {
            return await _userService.GetAll();
        }
        
        
        [HttpGet("{id}", Name = "GetUserPage")]
        public async Task<List<User>> GetPage(int id)
        {
            return (await _userService.GetAll()).GetRange(id * 10, 10 );
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<User> Get(int id)
        {
            return await _userService.GetById(id);
        }
        
        

        // POST: api/User
        [HttpPost]
        public async System.Threading.Tasks.Task  Post([FromBody] User user)
        {
            await _userService.Create(user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async System.Threading.Tasks.Task  Put(int id, [FromBody] User user)
        {
            await _userService.Update(user);
        }
        

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_userService.GetById(id) == null)
                return BadRequest("Bad input");
            
            await _userService.Delete(id);
            return Ok();
        }
    }
}
