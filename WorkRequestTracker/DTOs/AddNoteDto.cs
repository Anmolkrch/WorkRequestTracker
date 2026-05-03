using System.ComponentModel.DataAnnotations;

namespace WorkRequestTracker.DTOs
{
    public class AddNoteDto
    {
        [Required]
        public string Note { get; set; }
    }
}
