using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Services
{
    public interface ITaskRepository
    {
        Task<CustomTaskDto> CreateTaskAsync(CustomTaskDto taskDto);
        Task<IEnumerable<CustomTaskDto>> GetTasksByUserIdAsync(string userId);
        Task<IEnumerable<CustomTaskDto>> GetTasksByProjectIdAsync(string projectId);
        Task<bool> UpdateTaskAsync(string taskId, CustomTaskDto updatedTaskDto);
        Task<bool> DeleteTaskAsync(string taskId);
        Task SendTaskNotificationsAsync();
    }
}
