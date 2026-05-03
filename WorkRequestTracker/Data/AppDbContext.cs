using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkRequestTracker.Models;

namespace WorkRequestTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<WorkRequest> WorkRequests => Set<WorkRequest>();
        public DbSet<WorkRequestNote> WorkRequestNotes => Set<WorkRequestNote>();
    }
}
