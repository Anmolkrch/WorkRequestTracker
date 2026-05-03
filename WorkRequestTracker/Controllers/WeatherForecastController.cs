using Microsoft.AspNetCore.Mvc;
using WorkRequestTracker.DTOs;
using WorkRequestTracker.Interfaces;
using WorkRequestTracker.Models;
using WorkRequestTracker.Services;

namespace WorkRequestTracker.Controllers
{
    [ApiController]
    [Route("api/work-requests")]
    public class WorkRequestsController : ControllerBase
    {
        private readonly IWorkRequestService _service;

        public WorkRequestsController(IWorkRequestService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            string? status,
            string? search,
            int page = 1,
            int pageSize = 10)
        {
            var result = await _service.GetAllAsync(
                status,
                search,
                page,
                pageSize);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return NotFound(ApiError("Request not found"));

            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkRequestDto dto)
        {
            if (!Enum.TryParse<Priority>(dto.Priority, true, out var priority))
                return BadRequest(ApiError("Invalid priority"));

            if (!Enum.TryParse<Status>(dto.Status, true, out var status))
                return BadRequest(ApiError("Invalid status"));

            var request = new WorkRequest
            {
                Title = dto.Title,
                ClientName = dto.ClientName,
                Description = dto.Description,
                Priority = priority,
                Status = status,
                DueDate = dto.DueDate,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var created = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            UpdateStatusDto dto)
        {
            if (!Enum.TryParse<Status>(dto.Status, true, out var status))
                return BadRequest(ApiError("Invalid status"));

            var updated = await _service.UpdateStatusAsync(id, status);

            if (!updated)
                return NotFound(ApiError("Request not found"));

            return Ok(new
            {
                success = true,
                message = "Status updated successfully"
            });
        }

        [HttpPost("{id}/notes")]
        public async Task<IActionResult> AddNote(
            int id,
            AddNoteDto dto)
        {
            var added = await _service.AddNoteAsync(id, dto.Note);

            if (!added)
                return NotFound(ApiError("Request not found"));

            return Ok(new
            {
                success = true,
                message = "Note added successfully"
            });
        }

        private object ApiError(string message)
        {
            return new
            {
                success = false,
                message
            };
        }
    }
}