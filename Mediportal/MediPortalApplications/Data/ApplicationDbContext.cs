using MediPortalApplications.Models;
using Microsoft.EntityFrameworkCore;

namespace MediPortalApplications.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

       public DbSet<Application> Applications { get; set; }
    }
}
