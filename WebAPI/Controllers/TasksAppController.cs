using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    public class TasksAppController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksAppController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("task/all")]
        public IActionResult GetAllTasks()
        {
            return Ok(_taskService.GetAllTasks());          
        }

        [HttpGet("task/{id}")]
        public IActionResult GetTask(int id)
        {
            return Ok(_taskService.FindTask(id));      
        }

        [HttpPost("task/create")]
        public IActionResult CreateTask(string name, int currentUser, DateTime endDate)
        {            
             return Ok(_taskService.CreateTask(name, currentUser, endDate));
        }

        [HttpPost("task/delete")]
        public IActionResult DeleteTask(int id, int userId)
        {
            return Ok(_taskService.Delete(id, userId));
        }
        [HttpPost("task/reestablish")]
        public IActionResult ReestablishUser(int id, int userId)
        {
            return Ok(_taskService.Reestablish(id, userId));
        }
        [HttpPost("task/hardDelete")]
        public IActionResult HardDeleteUser(int id, int userId)
        {
            _taskService.HardDelete(id, userId);
            return Ok();
        }
    }
}
