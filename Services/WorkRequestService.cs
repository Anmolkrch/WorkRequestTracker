using Microsoft.EntityFrameworkCore;
using WorkRequestTracker.Data;
using WorkRequestTracker.Interfaces;
using WorkRequestTracker.Models;

namespace WorkRequestTracker.Services
{
    public class WorkRequestService : IWorkRequestService
    {
        private readonly AppDbContext _context;

        public WorkRequestService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkRequest>> GetAllAsync(
            string? status,
            string? search,
            int page,
            int pageSize)
        {
            var query = _context.WorkRequests
                .Include(x => x.Notes)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status) &&
                Enum.TryParse<Status>(status, true, out var parsedStatus))
            {
                query = query.Where(x => x.Status == parsedStatus);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Title.Contains(search) ||
                    x.ClientName.Contains(search));
            }

            return await query
                .OrderByDescending(x => x.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<WorkRequest?> GetByIdAsync(int id)
        {
            return await _context.WorkRequests
                .Include(x => x.Notes)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WorkRequest> CreateAsync(WorkRequest request)
        {
            _context.WorkRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> UpdateStatusAsync(int id, Status status)
        {
            var item = await _context.WorkRequests.FindAsync(id);

            if (item == null)
                return false;

            item.Status = status;
            item.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddNoteAsync(int id, string noteText)
        {
            var item = await _context.WorkRequests.FindAsync(id);

            if (item == null)
                return false;

            var note = new WorkRequestNote
            {
                WorkRequestId = id,
                Note = noteText,
                CreatedDate = DateTime.UtcNow
            };

            _context.WorkRequestNotes.Add(note);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}