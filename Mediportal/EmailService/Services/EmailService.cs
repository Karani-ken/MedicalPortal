
using MediPortalEmailService.Models;
using MediPortalEmailService.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace MediPortalEmailService.Services
{
    public class EmailService
    {
        private DbContextOptions<ApplicationDbContext> options;

        public EmailService()
        {
        }
        public EmailService(DbContextOptions<ApplicationDbContext> options)
        {
            this.options = options;
        }


        public async Task SaveData(EmailLoggers emailLoggers)
        {
            //create _context

            var _context = new ApplicationDbContext(this.options);
            _context.EmailLoggers.Add(emailLoggers);
            await _context.SaveChangesAsync();
        }
    }
}
