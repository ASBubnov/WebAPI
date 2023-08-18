using DAL.Interfaces;
using DAL.Models;

namespace Services
{
    public interface ITaskService
    {
        IEnumerable<TaskApp> GetAllTasks();
        TaskApp CreateTask(string name, int currentUser, DateTime endDate);
        TaskApp FindTask(int taskId);
        TaskApp Delete(int taskId, int userId);
        TaskApp Reestablish(int taskId, int userId);
        void HardDelete(int taskID, int userId);
    }

    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskApp> _repository;
        
        public TaskService(IRepository<TaskApp> repository)
        {
            _repository = repository;
        }

        public TaskApp CreateTask(string name, int currentUser, DateTime endDate)
        {
            var task = new TaskApp
            {
                Name = name,
                CreatorId = currentUser,
                AverageTime = endDate - DateTime.UtcNow,
                CreationDate = DateTime.UtcNow,
                IsAlive = true
            };
            return _repository.Create(task);
        }
        public TaskApp FindTask(int taskId)
        {
            return _repository.FindByID(taskId);            
        }

        public IEnumerable<TaskApp> GetAllTasks()
        {
            var task = new TaskApp();
            Func<TaskApp, bool> selector = task => task.IsAlive;
            return _repository.Find(selector);
        }
        public TaskApp Delete(int taskId, int userId)
        {
            var task = _repository.FindByID(taskId);
            if (task != null && task.CreatorId == userId)
            {
                task.IsAlive = false;
                return _repository.Update(task);
            }
            else
            {
                return null;
            }
        }

        public TaskApp Reestablish(int taskId, int userId)
        {
            var task = _repository.FindByID(taskId);
            if (task != null && task.CreatorId == userId)
            {
                task.IsAlive = true;
                return _repository.Update(task);
            }
            else
            {
                return null;
            }
        }

        public void HardDelete(int taskId, int userId)
        {
            var task = _repository.FindByID(taskId);
            if (task != null && task.CreatorId == userId)
            {
                _repository.Delete(taskId);
            }
        }
    }
}
