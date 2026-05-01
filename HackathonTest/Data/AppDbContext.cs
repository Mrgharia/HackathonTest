using HackathonTest.Models;
using Microsoft.EntityFrameworkCore;

namespace HackathonTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NominationRecord> NominationRecords { get; set; }
        public DbSet<PipelineMaster> PipelineMasters { get; set; }
        public DbSet<ShipperMaster> ShipperMasters { get; set; }
        public DbSet<DropDownMaster> DropdownMasters { get; set; }
    }
}