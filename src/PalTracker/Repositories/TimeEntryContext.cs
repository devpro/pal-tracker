using Microsoft.EntityFrameworkCore;
using PalTracker.Entities;

namespace PalTracker.Repositories
{
    public class TimeEntryContext : DbContext
    {
        public DbSet<TimeEntryRecord> TimeEntryRecords { get; set; }

        public TimeEntryContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
