using ApplicationsService.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

       public DbSet<Application> Applications { get; set; }
    }
}
