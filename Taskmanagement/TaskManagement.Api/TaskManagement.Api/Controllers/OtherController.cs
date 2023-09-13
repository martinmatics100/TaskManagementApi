using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Implementation;
using TaskManagement.Core.Services;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        private readonly IOtherRepository _otherRepository;

        public OtherController(IOtherRepository otherRepository)
        {
            _otherRepository = otherRepository;
        }

        [HttpGet("bystatus/{status}")]
        public async Task<IActionResult> GetTasksByStatus(string status)
        {
            try
            {
                var tasks = await _otherRepository.GetTasksByStatusAsync(status);

                if (tasks == null)
                {
                    return NotFound(); 
                }
                return Ok(tasks);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetTasksByStatus: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpGet("bypriority/{priority}")]
        public async Task<IActionResult> GetTasksByPriority(string priority)
        {
            try
            {
                var tasks = await _otherRepository.GetTasksByPriorityAsync(priority);

                if (tasks == null)
                {
                    return NotFound();
                }

                return Ok(tasks); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetTasksByPriority: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpGet("dueforcurrentweek")]
        public async Task<IActionResult> GetTasksDueForCurrentWeek()
        {
            try
            {
                var tasks = await _otherRepository.GetTasksDueForCurrentWeekAsync();

                if (tasks == null)
                {
                    return NotFound(); 
                }

                return Ok(tasks); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetTasksDueForCurrentWeek: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpPost("assigntoproject/{taskId}/{projectId}")]
        public async Task<IActionResult> AssignTaskToProject(string taskId, string projectId)
        {
            try
            {
                var result = await _otherRepository.AssignTaskToProjectAsync(taskId, projectId);

                if (!result)
                {
                    return NotFound("project not found"); 
                }

                return Ok("Task successfully assigned"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in AssignTaskToProject: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpPost("removefromproject/{taskId}")]
        public async Task<IActionResult> RemoveTaskFromProject(string taskId)
        {
            try
            {
                var result = await _otherRepository.RemoveTaskFromProjectAsync(taskId);

                if (!result)
                {
                    return NotFound("Task not found"); 
                }

                return Ok("Task successfully removed"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in RemoveTaskFromProject: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpPost("markasread/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(string notificationId)
        {
            try
            {
                var result = await _otherRepository.MarkNotificationAsReadAsync(notificationId);

                if (!result)
                {
                    return NotFound("Notification not found"); 
                }

                return Ok("Notification successfully read"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in MarkNotificationAsRead: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpPost("markasunread/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsUnread(string notificationId)
        {
            try
            {
                var result = await _otherRepository.MarkNotificationAsUnreadAsync(notificationId);

                if (!result)
                {
                    return NotFound("Notification not found"); 
                }

                return Ok(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in MarkNotificationAsUnread: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }
    }
}
