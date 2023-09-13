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

namespace TaskManagement.Core.Implementation
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly TaskManagementDbContext _context;

        public NotificationRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task CreateNotificationAsync(NotificationDto notificationDto)
        {
            try
            {
                var notification = new Notification
                {
                    NotificationId = Guid.NewGuid().ToString(),
                    Type = notificationDto.Type,
                    Message = notificationDto.Message,
                    Timestamp = notificationDto.Timestamp,
                    UserId = notificationDto.UserId,
                    TaskId = notificationDto.TaskId
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating a notification: {ex}");
                throw;
            }
        }

        public async Task<NotificationDto> GetNotificationByIdAsync(string notificationId)
        {
            try
            {
                var notification = await _context.Notifications
                    .Where(n => n.NotificationId == notificationId)
                    .Select(n => new NotificationDto
                    {
                        Type = n.Type,
                        Message = n.Message,
                        Timestamp = n.Timestamp,
                        UserId = n.UserId,
                        TaskId = n.TaskId
                    })
                    .FirstOrDefaultAsync();

                return notification;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving a notification by ID: {ex}");
                throw;
            }
        }

        public async Task<List<NotificationDto>> GetAllNotificationsAsync()
        {
            try
            {
                var notifications = await _context.Notifications
                    .Select(n => new NotificationDto
                    {
                        Type = n.Type,
                        Message = n.Message,
                        Timestamp = n.Timestamp,
                        UserId = n.UserId,
                        TaskId = n.TaskId
                    })
                    .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving all notifications: {ex}");
                throw;
            }
        }

        public async Task UpdateNotificationAsync(string notificationId, NotificationDto updatedNotificationDto)
        {
            try
            {
                var notification = await _context.Notifications
                    .Where(n => n.NotificationId == notificationId)
                    .FirstOrDefaultAsync();

                if (notification != null)
                {
                    notification.Type = updatedNotificationDto.Type;
                    notification.Message = updatedNotificationDto.Message;
                    notification.Timestamp = updatedNotificationDto.Timestamp;

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating a notification: {ex}");
                throw;
            }
        }

        public async Task DeleteNotificationAsync(string notificationId)
        {
            try
            {
                var notification = await _context.Notifications
                    .Where(n => n.NotificationId == notificationId)
                    .FirstOrDefaultAsync();

                if (notification != null)
                {
                    _context.Notifications.Remove(notification);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting a notification: {ex}");
                throw;
            }
        }
    }
}
