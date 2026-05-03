using System.ComponentModel.DataAnnotations;

namespace WorkRequestTracker.DTOs
{
    public class UpdateStatusDto
    {
        [Required]
        public string Status { get; set; }
    }
}
