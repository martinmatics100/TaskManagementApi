using AutoMapper;
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
    public class UserRepository : IUserRepository
    {
        private readonly TaskManagementDbContext _context;

        public UserRepository(TaskManagementDbContext context)
        {
            _context = context; 
        }

   
        public async Task<bool> CreateUserAsync(UserDto userDto)
        {
            try
            {
                if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
                {
                    return false;
                }

                var usercreated = new User
                {
                    UserId = Guid.NewGuid().ToString(),
                    Name = userDto.Name,
                    Email = userDto.Email,
                    
                };

                _context.Users.Add(usercreated);
                await _context.SaveChangesAsync();

              
                return true;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while creating a user. {ex}");
                return false;
            }
        }



       
        public async Task<UserDto> ReadUserAsync(string userId)
        {
            try
            {
                var user = await _context.Users
                    .Where(u => u.UserId == userId && !u.IsDeleted) 
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return null;
                }

                var userDto = new UserDto
                {
                    Name = user.Name,
                    Email = user.Email,
                };

                return userDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading a user. {ex}");

                return null;
            }
        }


        public async Task<IEnumerable<UserDto>> ReadAllUsersAsync()
        {
            try
            {
                var users = await _context.Users
                    .Where(u => !u.IsDeleted) 
                    .Select(user => new UserDto
                    {
                        Name = user.Name,
                        Email = user.Email,
                    })
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading all users. {ex}");

                return Enumerable.Empty<UserDto>();
            }
        }

        //UPDATE METHOD
        public async Task<bool> UpdateUserDetailsAsync(string userId, UserDto updatedUserDto)
        {
            try
            {
                var user = await _context.Users
            .Where(u => u.UserId == userId && !u.IsDeleted) 
            .FirstOrDefaultAsync();

                if (user == null)
                {
                    return false; 
                }

                var existingUserWithEmail = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId != userId && u.Email == updatedUserDto.Email);

                if (existingUserWithEmail != null)
                {
                    return false; 
                }

                user.Name = updatedUserDto.Name;
                user.Email = updatedUserDto.Email;
               
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user details: {ex}");

                throw;
            }
        }


     
        public async Task<bool> DeleteUserByIdAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);

                if (user == null)
                {
                    return false; 
                }

                user.IsDeleted = true;
                await _context.SaveChangesAsync();

                return true; 
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Error deleting user: {ex}");

                return false;
            }
        }

    }
}
