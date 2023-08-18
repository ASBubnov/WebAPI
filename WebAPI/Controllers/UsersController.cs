using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("users/all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("user/{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(_userService.FindUser(id));
        }

        [HttpPost("user/create")]
        public IActionResult CreateUser(string firstName, string middleName, string lastName, string email)
        {
            return Ok(_userService.CreateUser(firstName, middleName, lastName, email));
        }

        [HttpPost("user/delete")]
        public IActionResult DeleteUser(int id)
        {
            return Ok(_userService.Delete(id));
        }
        [HttpPost("user/reestablish")]
        public IActionResult ReestablishUser(int id)
        {
            return Ok(_userService.Reestablish(id));
        }
        [HttpPost("user/hardDelete")]
        public IActionResult HardDeleteUser(int id)
        {
            _userService.HardDelete(id);
            return Ok();
        }
    }
}
