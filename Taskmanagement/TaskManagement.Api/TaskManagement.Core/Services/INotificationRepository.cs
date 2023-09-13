using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Services
{
    public interface INotificationRepository
    {
        Task CreateNotificationAsync(NotificationDto notificationDto);
        Task<NotificationDto> GetNotificationByIdAsync(string notificationId);
        Task<List<NotificationDto>> GetAllNotificationsAsync();
        Task UpdateNotificationAsync(string notificationId, NotificationDto updatedNotificationDto);
        Task DeleteNotificationAsync(string notificationId);
    }
}
