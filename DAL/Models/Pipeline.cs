
namespace DAL.Models
{
    public class Pipeline
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public TimeSpan AverageTime { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? StartExecutedDate { get; set; }

        public DateTime? DeleteDate { get; set; }
        
        public int CreatorId { get; set; }

        public List<int> TasksId { get; set; }

        public bool IsAlive { get; set; }
        public bool IsExecuted { get; set; }
    }
}
