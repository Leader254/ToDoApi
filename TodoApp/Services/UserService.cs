using Microsoft.EntityFrameworkCore;
using TodoApp.Context;
using TodoApp.Models;
using TodoApp.Responses;
using TodoApp.Services.IService;

namespace TodoApp.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;

        public UserService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> AddUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return "User added successfully";
        }

        public async Task<string> DeleteUserAsync(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return "User deleted successfully";
            }
            return "User not found";
        }

        // return a user with their todos
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var result = await context.Users.Select(user => new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Occupation = user.Occupation,
                Todos = user.Todos.Select(todo => new Todo
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    CreatedAt = todo.CreatedAt,
                    IsComplete = todo.IsComplete,
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == id);
            return result;

        }

        public async Task<IEnumerable<TodoUser>> GetUsersAsync()
        {
            return await context.Users.Select(user => new TodoUser
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Occupation = user.Occupation,
                Todos = user.Todos.Select(todo => new TodoResponseDto
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Description = todo.Description,
                    CreatedAt = todo.CreatedAt,
                    IsComplete = todo.IsComplete,
                }).ToList()
            }).ToListAsync();

        }

        public async Task<string> UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return "User updated successfully";
        }
    }
}