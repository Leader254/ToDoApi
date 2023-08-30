using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services.IService
{
    public interface ITodoService
    {
        Task<string> AddTodoAsync(Todo todo);
        Task<string> DeleteTodoAsync(Guid id);
        Task<string> UpdateTodoAsync(Todo todo);
        Task<IEnumerable<Todo>> GetTodosAsync();
        Task<Todo> GetTodoByIdAsync(Guid id);
    }
}