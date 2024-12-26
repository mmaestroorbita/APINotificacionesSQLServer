using Notifications.DTO;

namespace NotificationService.Services
{
    public interface IReportService
    {
       Task<ServiceResponse<bool>> ProcessEventAsync(EventDto evento);
    }
}