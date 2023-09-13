using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Services
{
    public interface IOtherRepository
    {
        Task<IEnumerable<CustomTaskDto>> GetTasksByStatusAsync(string status);
        Task<IEnumerable<CustomTaskDto>> GetTasksByPriorityAsync(string priority);
        Task<IEnumerable<CustomTaskDto>> GetTasksDueForCurrentWeekAsync();
        Task<bool> AssignTaskToProjectAsync(string taskId, string projectId);
        Task<bool> RemoveTaskFromProjectAsync(string taskId);
        Task<bool> MarkNotificationAsReadAsync(string notificationId);
        Task<bool> MarkNotificationAsUnreadAsync(string notificationId);
    }
}
