using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Services;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationController(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDto notificationDto)
        {
            try
            {
                if (notificationDto == null)
                {
                    return BadRequest("Invalid notification data");
                }

                await _notificationRepository.CreateNotificationAsync(notificationDto);

                return Ok("Notification created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpGet("{notificationId}")]
        public async Task<IActionResult> GetNotificationById(string notificationId)
        {
            try
            {
                if (string.IsNullOrEmpty(notificationId))
                {
                    return BadRequest("NotificationId is required");
                }

                var notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);

                if (notification == null)
                {
                    return NotFound("Notification not found");
                }

                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotifications()
        {
            try
            {
                var notifications = await _notificationRepository.GetAllNotificationsAsync();

                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{notificationId}")]
        public async Task<IActionResult> UpdateNotificationById(string notificationId, [FromBody] NotificationDto updatedNotificationDto)
        {
            try
            {
                if (string.IsNullOrEmpty(notificationId))
                {
                    return BadRequest("NotificationId is required");
                }

                var existingNotification = await _notificationRepository.GetNotificationByIdAsync(notificationId);

                if (existingNotification == null)
                {
                    return NotFound("Notification not found");
                }

                await _notificationRepository.UpdateNotificationAsync(notificationId, updatedNotificationDto);

                return Ok("Notification updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotificationById(string notificationId)
        {
            try
            {
                if (string.IsNullOrEmpty(notificationId))
                {
                    return BadRequest("NotificationId is required");
                }

                var existingNotification = await _notificationRepository.GetNotificationByIdAsync(notificationId);

                if (existingNotification == null)
                {
                    return NotFound("Notification not found");
                }
                await _notificationRepository.DeleteNotificationAsync(notificationId);

                return Ok("Notification deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
