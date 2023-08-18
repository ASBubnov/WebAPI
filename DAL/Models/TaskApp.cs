using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class TaskApp
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan AverageTime { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        [Required]
        public int CreatorId { get; set; }

        public List<int>? ExecutorsId { get; set; }

        public bool IsAlive { get; set; }
    }
}
