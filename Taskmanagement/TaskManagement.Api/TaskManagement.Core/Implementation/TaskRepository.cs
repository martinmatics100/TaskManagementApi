using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Services;
using TaskManagement.Data.AppDbContext;
using TaskManagement.Data.Entities;
using TaskManagement.Data.EntityEnums;

namespace TaskManagement.Core.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDbContext _context;
        private readonly NotificationService _notificationService;

        public TaskRepository(TaskManagementDbContext context, NotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;

        }

        public async Task<CustomTaskDto> CreateTaskAsync(CustomTaskDto taskDto)
        {
            try
            {
                DateTime dueDate = DateTime.Now.AddHours(48);

                var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == taskDto.ProjectId && !p.IsDeleted);
                var userExists = await _context.Users.AnyAsync(u => u.UserId == taskDto.CreatedByUserId && !u.IsDeleted);

                if (!projectExists || !userExists)
                {
                    return null;
                }

                var task = new CustomTask
                {
                    CustomTaskId = Guid.NewGuid().ToString(),
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    DueDate = dueDate,
                    Priority = taskDto.Priority,
                    Status = taskDto.Status,
                    ProjectId = taskDto.ProjectId,
                    CreatedByUserId = taskDto.CreatedByUserId
                };

                var addedTask = _context.Tasks.Add(task).Entity;
                await _context.SaveChangesAsync();

                return new CustomTaskDto
                {
                    Title = addedTask.Title,
                    Description = addedTask.Description,
                    DueDate = addedTask.DueDate,
                    Priority = addedTask.Priority,
                    Status = addedTask.Status,
                    ProjectId = addedTask.ProjectId,
                    CreatedByUserId = addedTask.CreatedByUserId
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating a task. {ex}");

                return null;
            }
        }



        public async Task<IEnumerable<CustomTaskDto>> GetTasksByUserIdAsync(string userId)
        {
            try
            {
                var tasks = await _context.Tasks
                    .Where(t => t.CreatedByUserId == userId && t.Project.IsDeleted)
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
                Console.WriteLine($"An error occurred while retrieving tasks by user ID. {ex}");

                return Enumerable.Empty<CustomTaskDto>();
            }
        }


        public async Task<IEnumerable<CustomTaskDto>> GetTasksByProjectIdAsync(string projectId)
        {
            try
            {
                var isProjectDeleted = await _context.Projects
                    .Where(p => p.ProjectId == projectId)
                    .Select(p => p.IsDeleted)
                    .FirstOrDefaultAsync();

                if (isProjectDeleted)
                {
                    return new List<CustomTaskDto>();
                }
                var tasks = await _context.Tasks
                    .Where(t => t.ProjectId == projectId)
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
                Console.WriteLine($"An error occurred while retrieving tasks by project ID.{ex}");

                return Enumerable.Empty<CustomTaskDto>();
            }
        }


        public async Task<bool> UpdateTaskAsync(string taskId, CustomTaskDto updatedTaskDto)
        {
            try
            {
                DateTime dueDate = DateTime.Now.AddHours(48);

                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.CustomTaskId == taskId && t.Project.IsDeleted);

                if (task == null)
                {
                    return false; 
                }
                task.Title = updatedTaskDto.Title;
                task.Description = updatedTaskDto.Description;
                task.DueDate = dueDate;
                task.Priority = updatedTaskDto.Priority;
                task.Status = updatedTaskDto.Status;
                task.ProjectId = updatedTaskDto.ProjectId;
                task.CreatedByUserId = updatedTaskDto.CreatedByUserId;

                var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == updatedTaskDto.ProjectId);
                var userExists = await _context.Users.AnyAsync(u => u.UserId == updatedTaskDto.CreatedByUserId);

                if (!projectExists || !userExists)
                {
                    return false; 
                }

                await _context.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating a task. {ex}");

                return false;
            }
        }


        public async Task<bool> DeleteTaskAsync(string taskId)
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.CustomTaskId == taskId);

                if (task == null)
                {
                    return false; 
                }

                task.IsDeleted = true;

                await _context.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting a task. {ex}");

                return false;
            }
        }


        public async Task SendTaskNotificationsAsync()
        {
            try
            {
                var currentDate = DateTime.Now.Date;
                var tasks = await _context.Tasks
                    .Where(t => t.DueDate.Date <= currentDate && t.Status != Status.Completed)
                    .ToListAsync();

                foreach (var task in tasks)
                {
                    var message = $"Task '{task.Title}' is due on {task.DueDate}. Current status: {task.Status}";

                    _notificationService.SendNotification(task.CreatedByUserId, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending task notifications. {ex}");

            }
        }
    }
}
