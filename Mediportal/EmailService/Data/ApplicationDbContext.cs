using EmailService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailService.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}
