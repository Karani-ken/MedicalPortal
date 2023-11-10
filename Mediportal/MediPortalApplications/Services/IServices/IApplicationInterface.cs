using MediPortalApplications.Models;

namespace MediPortalApplications.Services.IServices
{
    public interface IApplicationInterface
    {
        Task<string> makeApplication(Application application);
        Task<Application> getApplication(Guid Id);
        Task<string> DeleteApplication(Guid Id);

        Task<List<Application>> getApplications();
    }
}
