using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Services;
using TaskManagement.Data.AppDbContext;
using TaskManagement.Data.EntityEnums;

namespace TaskManagement.Core.Implementation
{
    public class OtherRepository : IOtherRepository
    {
        private readonly TaskManagementDbContext _taskManagementDbContext;

        public OtherRepository(TaskManagementDbContext taskManagementDbContext)
        {
            _taskManagementDbContext = taskManagementDbContext;
        }

        public async Task<IEnumerable<CustomTaskDto>> GetTasksByStatusAsync(string status)
        {
            try
            {
                if (!Enum.TryParse<Status>(status, out var statusEnum))
                {
                    return Enumerable.Empty<CustomTaskDto>();
                }

                var tasks = await _taskManagementDbContext.Tasks
                    .Where(t => t.Status == statusEnum && !t.IsDeleted) 
                    .Select(task => new CustomTaskDto
                    {
                        Title = task.Title,
                        Description = task.Description,
                        DueDate = task.DueDate,
                        Priority = task.Priority,
                        Status = task.Status,
                        ProjectId = task.ProjectId,
                        CreatedByUserId = task.CreatedByUserId
                    })
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving tasks by status. {ex}");

                return Enumerable.Empty<CustomTaskDto>(); 
            }
        }



        public async Task<IEnumerable<CustomTaskDto>> GetTasksByPriorityAsync(string priority)
        {
            try
            {
                if (!Enum.TryParse<Priority>(priority, out var priorityEnum))
                {
                    return Enumerable.Empty<CustomTaskDto>(); 
                }

                var tasks = await _taskManagementDbContext.Tasks
                    .Where(t => t.Priority == priorityEnum && !t.IsDeleted)
                    .Select(task => new CustomTaskDto
                    {
                        Title = task.Title,
                        Description = task.Description,
                        DueDate = task.DueDate,
                        Priority = task.Priority,
                        Status = task.Status,
                        ProjectId = task.ProjectId,
                        CreatedByUserId = task.CreatedByUserId
                    })
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving tasks by priority. {ex}");

                return Enumerable.Empty<CustomTaskDto>(); 
            }
        }


        public async Task<IEnumerable<CustomTaskDto>> GetTasksDueForCurrentWeekAsync()
        {
            try
            {
                DateTime today = DateTime.Now;
                DateTime startOfWeek = today.AddDays(-((int)today.DayOfWeek - (int)DayOfWeek.Monday));
                DateTime endOfWeek = startOfWeek.AddDays(6);

                var tasks = await _taskManagementDbContext.Tasks
                    .Where(t => t.DueDate >= startOfWeek && t.DueDate <= endOfWeek && !t.IsDeleted)
                    .Select(task => new CustomTaskDto
                    {
                        Title = task.Title,
                        Description = task.Description,
                        DueDate = task.DueDate,
                        Priority = task.Priority,
                        Status = task.Status,
                        ProjectId = task.ProjectId,
                        CreatedByUserId = task.CreatedByUserId
                    })
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving tasks due for the current week. {ex}");

                return Enumerable.Empty<CustomTaskDto>();
            }
        }


        public async Task<bool> AssignTaskToProjectAsync(string taskId, string projectId)
        {
            try
            {
                var task = await _taskManagementDbContext.Tasks.FirstOrDefaultAsync(t => t.CustomTaskId == taskId);

                if (task == null)
                {
                    return false; 
                }

                task.ProjectId = projectId;

                await _taskManagementDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while assigning task {taskId} to project {projectId}.");

                return false; 
            }
        }


        public async Task<bool> RemoveTaskFromProjectAsync(string taskId)
        {
            try
            {
                var task = await _taskManagementDbContext.Tasks.FirstOrDefaultAsync(t => t.CustomTaskId == taskId);

                if (task == null)
                {
                    return false; 
                }

                task.ProjectId = null;

                await _taskManagementDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing task {taskId} from its project.");

                return false; 
            }
        }


        public async Task<bool> MarkNotificationAsReadAsync(string notificationId)
        {
            try
            {
                var notification = await _taskManagementDbContext.Notifications.FirstOrDefaultAsync(n => n.NotificationId == notificationId);

                if (notification == null)
                {
                    return false;
                }

                notification.IsRead = true;

                await _taskManagementDbContext.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while marking notification {notificationId} as read.");
                return false;
            }
        }

        public async Task<bool> MarkNotificationAsUnreadAsync(string notificationId)
        {
            try
            {
                var notification = await _taskManagementDbContext.Notifications.FirstOrDefaultAsync(n => n.NotificationId == notificationId);

                if (notification == null)
                {
                    return false; 
                }

                notification.IsRead = false;

                await _taskManagementDbContext.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while marking notification {notificationId} as unread.");
                return false;
            }
        }
    }
}
