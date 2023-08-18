using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Services;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    public class PipelineController : Controller
    {
        private IPipelineService _pipelineService;
        public PipelineController(IPipelineService pipelineService)
        {
            _pipelineService = pipelineService;
        }

        [HttpGet("pipeline/all")]
        public IActionResult GetAllPipelines()
        {
            return Ok(_pipelineService.GetAllPipelines());
        }

        [HttpPost("pipeline/create")]
        public IActionResult CreatePipeline(int currentUser, List<int> tasksId, string name)
        {
            return Ok(_pipelineService.CreatePipeline(currentUser,tasksId,name));
        }

        [HttpGet("pipeline/{pipelineId}")]
        public IActionResult FindPipeline([FromRoute]int pipelineId)
        {
            return Ok(_pipelineService.FindPipeline(pipelineId));
        }

        [HttpPost("pipeline/delete")]
        public IActionResult Delete(int pipelineId, int userId)
        {
            return Ok(_pipelineService.Delete(pipelineId,userId));
        }

        [HttpPost("pipeline/reestablish")]
        public IActionResult Reestablish(int pipelineId, int userId)
        {
            return Ok(_pipelineService.Reestablish(pipelineId,userId));
        }

        [HttpPost("pipeline/hardDelete")]
        public IActionResult HardDelete(int pipelineID, int userId)
        {
            _pipelineService.HardDelete(pipelineID,userId);
            return Ok();
        }

        [HttpPost("pipeline/run")]
        public IActionResult Run(int pipelineId)
        {
            return Ok(_pipelineService.RunExternal(pipelineId));
        }

    }
}
