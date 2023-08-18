using DAL.Interfaces;
using DAL.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace Services
{
    public interface IPipelineService
    {
        IEnumerable<Pipeline> GetAllPipelines();
        Pipeline CreatePipeline(int currentUser, List<int> tasksId, string name);
        Pipeline FindPipeline(int pipelineId);
        Pipeline Delete(int pipelineId, int userId);
        Pipeline Reestablish(int pipelineId, int userId);
        void HardDelete(int pipelineID, int userId);
        Pipeline Run(int pipelineId);
        string RunExternal(int pipelineId);
    }

    public class PipeLineServiceConfig
    {
       public string Path { get; set; }
    }

    public class PipelineService : IPipelineService
    {
        private readonly IRepository<Pipeline> _repository;
        private readonly ITaskService _taskService;
        private readonly PipeLineServiceConfig _config;
        public PipelineService(IRepository<Pipeline> repository, ITaskService taskService, IOptions<PipeLineServiceConfig> options)
        {
            _repository = repository;
            _taskService = taskService;
            _config = options.Value;
        }

        public Pipeline CreatePipeline(int currentUser, List<int> tasksId, string name)
        {
                        
            var pipeline = new Pipeline()
            {
                Name = name,
                CreatorId = currentUser,
                TasksId = tasksId,                
                IsAlive = true,
                CreationDate = DateTime.UtcNow,
                IsExecuted = false,
            };
            return _repository.Create(pipeline);
        }

        public Pipeline Delete(int pipelineId, int userId)
        {
            var pipeline = _repository.FindByID(pipelineId);
            if (pipeline != null && pipeline.CreatorId == userId)
            {
                pipeline.IsAlive = false;
                pipeline.IsExecuted = false;
                pipeline.DeleteDate = DateTime.UtcNow;
                return _repository.Update(pipeline);
            }
            else
            {
                return null;
            }
        }

        public Pipeline FindPipeline(int pipelineId)
        {
            return _repository.FindByID(pipelineId);
        }

        public IEnumerable<Pipeline> GetAllPipelines()
        {
            var pipeline = new Pipeline();
            Func<Pipeline, bool> selector = pipeline => pipeline.IsAlive;
            return _repository.Find(selector);
        }

        public void HardDelete(int pipelineID, int userId)
        {
            var pipeline = _repository.FindByID(pipelineID);
            if (pipeline != null && pipeline.CreatorId == userId)
            {
                _repository.Delete(pipelineID);
            }
        }

        public Pipeline Reestablish(int pipelineId, int userId)
        {
            throw new NotImplementedException();
        }

        public Pipeline Run(int pipelineId)
        {
            var pipeline = _repository.FindByID(pipelineId);
            pipeline.StartExecutedDate = DateTime.UtcNow;
            pipeline.AverageTime = GetSumOfAverageDateTimeTasks(pipeline.TasksId);
            _repository.Update(pipeline);
            return pipeline;
        }

        public string RunExternal(int pipelineId)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(_config.Path);
            startInfo.Arguments = pipelineId.ToString();
            startInfo.RedirectStandardOutput = true;
            var proc = Process.Start(startInfo);
            string line = string.Empty;            
            while (proc != null && !proc.StandardOutput.EndOfStream)
            {
                line = proc.StandardOutput.ReadLine();
            }
            var pipeline = _repository.FindByID(pipelineId);
            return $"Pipeline {pipeline.Name} finished \nAverageTime: {line}";
        }

        private TimeSpan GetSumOfAverageDateTimeTasks(List<int> tasksId)
        {
            var averageTimeTotal = new TimeSpan();
            foreach (var item in tasksId)
            {
                var task = _taskService.FindTask(item);
                if (task != null)
                {
                    averageTimeTotal += task.AverageTime;
                }
            }
            return averageTimeTotal;
        }
    }
}
