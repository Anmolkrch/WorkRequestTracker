using System.ComponentModel.DataAnnotations;

namespace WorkRequestTracker.DTOs
{
    public class CreateWorkRequestDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Priority { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime DueDate { get; set; }
    }
}
