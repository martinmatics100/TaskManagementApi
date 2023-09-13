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
    public class ProjectRepository : IProjectRepository
    {
        private readonly TaskManagementDbContext _context;

        public ProjectRepository(TaskManagementDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto)
        {
            try
            {
                var project = new Project
                {
                    ProjectId = Guid.NewGuid().ToString(),
                    Name = projectDto.Name,
                    Description = projectDto.Description
                };

                if (!_context.Projects.Any(p => p.ProjectId == project.ProjectId))
                {
                    var addToDb = _context.Projects.Add(project);
                    var saveChanges = await _context.SaveChangesAsync();
                }

                return new ProjectDto
                {
                    Name = project.Name,
                    Description = project.Description
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating a project.");

                return null;
            }
        }


        public async Task<ProjectDto> GetProjectByIdAsync(string projectId)
        {
            try
            {
                var project = await _context.Projects
                    .Where(p => p.ProjectId == projectId && !p.IsDeleted)
                    .Select(p => new ProjectDto
                    {
                        Name = p.Name,
                        Description = p.Description
                    })
                    .FirstOrDefaultAsync();

                return project;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving project with ID {projectId}.");

                return null; 
            }
        }


        public async Task<ProjectDto> UpdateProjectAsync(string projectId, ProjectDto updatedProjectDto)
        {
            try
            {
                var existingProject = await _context.Projects.FindAsync(projectId);

                if (existingProject == null)
                {
                    return null; 
                }

                if (existingProject.IsDeleted)
                {
                    return null;
                }

                existingProject.Name = updatedProjectDto.Name;
                existingProject.Description = updatedProjectDto.Description;

                await _context.SaveChangesAsync();

                return new ProjectDto
                {
                    Name = existingProject.Name,
                    Description = existingProject.Description
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating project with ID {projectId}.");

                return null; 
            }
        }


        public async Task<bool> DeleteProjectAsync(string projectId)
        {
            try
            {
                var project = await _context.Projects.FindAsync(projectId);

                if (project == null)
                {
                    return false; 
                }

                project.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting project with ID {projectId}.");

                return false; 
            }
        }

    }
}
