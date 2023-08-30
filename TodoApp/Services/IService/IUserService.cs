using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;
using TodoApp.Responses;

namespace TodoApp.Services.IService
{
    public interface IUserService
    {
        // Add user
        Task<string> AddUserAsync(User user);
        // Delete user
        Task<string> DeleteUserAsync(Guid id);
        // Update user
        Task<string> UpdateUserAsync(User user);
        // Get all users
        Task<IEnumerable<TodoUser>> GetUsersAsync();
        // Get user by id
        Task<User> GetUserByIdAsync(Guid id);
    }
}