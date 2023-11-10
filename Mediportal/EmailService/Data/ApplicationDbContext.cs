using MediPortalEmailService.Models;
using Microsoft.EntityFrameworkCore;

namespace MediPortalEmailService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}
