using System.ComponentModel.DataAnnotations;

namespace ASP.NETWebAPI.Models
{
    public class StudentInfo
    {
        public int studentId { get; set; }
        [Required]
        public string name { get; set; }
        [Range(0, 100, ErrorMessage = "Mark must be between 0 and 100.")]
        public double mark { get; set; }
    }
}
