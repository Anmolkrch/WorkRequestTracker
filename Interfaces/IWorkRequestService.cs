using WorkRequestTracker.Models;

namespace WorkRequestTracker.Interfaces
{
    public interface IWorkRequestService
    {
        Task<List<WorkRequest>> GetAllAsync(
            string? status,
            string? search,
            int page,
            int pageSize);

        Task<WorkRequest?> GetByIdAsync(int id);

        Task<WorkRequest> CreateAsync(WorkRequest request);

        Task<bool> UpdateStatusAsync(int id, Status status);

        Task<bool> AddNoteAsync(int id, string note);
    }
}