using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Services;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost]
        public async Task<ActionResult<CustomTaskDto>> CreateTask([FromBody] CustomTaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest("Invalid task data");
            }

            var createdTask = await _taskRepository.CreateTaskAsync(taskDto);

            if (createdTask == null)
            {
                return NotFound("Project or user not found, or task creation failed");
            }

            return Ok($"Task created for user with Id {taskDto.CreatedByUserId}");
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CustomTaskDto>>> GetTasksByUserId(string userId)
        {
            try
            {
                var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound($"No tasks found for user with ID {userId}");
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<CustomTaskDto>>> GetTasksByProjectId(string projectId)
        {
            try
            {
                var tasks = await _taskRepository.GetTasksByProjectIdAsync(projectId);

                if (tasks == null || !tasks.Any())
                {
                    return NotFound($"No tasks found for project with ID {projectId}");
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{taskId}")]
        public async Task<ActionResult<bool>> UpdateTask(string taskId, [FromBody] CustomTaskDto updatedTaskDto)
        {
            try
            {
                var result = await _taskRepository.UpdateTaskAsync(taskId, updatedTaskDto);

                if (!result)
                {
                    return NotFound($"Task with ID {taskId} not found or project not found.");
                }

                return Ok("Task updated successfully"); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult<bool>> DeleteTask(string taskId)
        {
            try
            {
                var result = await _taskRepository.DeleteTaskAsync(taskId);

                if (!result)
                {
                    return NotFound($"Task with ID {taskId} not found.");
                }

                return Ok("Task successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("sendtasknotifications")]
        public async Task<IActionResult> SendTaskNotifications()
        {
            try
            {
                await _taskRepository.SendTaskNotificationsAsync();
                return Ok("Task notifications sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error sending task notifications: {ex.Message}");
            }
        }
    }
}
