using ApplicationsService.Data;
using ApplicationsService.Models;
using ApplicationsService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ApplicationsService.Services
{
    public class ApplicationService : IApplicationInterface
    {
        private readonly ApplicationDbContext _context;
        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteApplication(Guid Id)
        {
           var ApplicationToDelete = await _context.Applications.FirstOrDefaultAsync(x => x.ApplicationId == Id);
            _context.Applications.Remove(ApplicationToDelete);
            await _context.SaveChangesAsync();
            return "Deleted successfully";
        }

        public async Task<Application> getApplication(Guid Id)
        {
            return await _context.Applications.FirstOrDefaultAsync(x => x.ApplicationId == Id);
        }

        public async Task<List<Application>> getApplications()
        {
            return await _context.Applications.ToListAsync();
        }

        public async Task<string> makeApplication(Application application)
        {
           await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
            return "Application was recieved successfully";
        }
    }
}
