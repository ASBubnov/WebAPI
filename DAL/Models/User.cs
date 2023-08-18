using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
        public bool isBlocked { get; set; }
    }
}
