using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Dto;
using TaskManagement.Core.Implementation;
using TaskManagement.Core.Services;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] ProjectDto projectDto)
        {
            try
            {
                if (projectDto == null)
                {
                    return BadRequest("Invalid project data");
                }

                var createdProject = await _projectRepository.CreateProjectAsync(projectDto);

                if (createdProject == null)
                {
                    return NotFound("Project not found");
                }

                return Ok("Project created successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in CreateProject: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(string projectId)
        {
            try
            {
                var project = await _projectRepository.GetProjectByIdAsync(projectId);

                if (project == null)
                {
                    return NotFound($"Project with Id {projectId} not found");
                }

                return Ok(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetProjectById: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult<ProjectDto>> UpdateProject(string projectId, [FromBody] ProjectDto updatedProjectDto)
        {
            try
            {
                if (updatedProjectDto == null || string.IsNullOrWhiteSpace(projectId))
                {
                    return BadRequest("Invalid project data or projectId");
                }

                var updatedProject = await _projectRepository.UpdateProjectAsync(projectId, updatedProjectDto);

                if (updatedProject == null)
                {
                    return NotFound($"Project with Id {projectId} not found");
                }

                return Ok("Project updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in UpdateProject: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteProject(string projectId)
        {
            try
            {
                var projectDeleted = await _projectRepository.DeleteProjectAsync(projectId);

                if (!projectDeleted)
                {
                    return NotFound($"Project with Id {projectId} not found");
                }

                return Ok("Project deleted successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in DeleteProject: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred.");
            }
        }
    }
}
