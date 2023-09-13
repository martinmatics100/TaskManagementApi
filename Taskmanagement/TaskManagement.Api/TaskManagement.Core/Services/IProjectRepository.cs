using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.Dto;

namespace TaskManagement.Core.Services
{
    public interface IProjectRepository
    {
        Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto);
        Task<ProjectDto> GetProjectByIdAsync(string projectId);
        Task<ProjectDto> UpdateProjectAsync(string projectId, ProjectDto updatedProjectDto);
        Task<bool> DeleteProjectAsync(string projectId);
    }
}
