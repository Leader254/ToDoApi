using TodoApp.Models;
using TodoApp.Responses;

namespace TodoApp.Services.IService
{
    public interface ITodoService
    {
        Task<string> AddTodoAsync(Todo todo);
        Task<string> DeleteTodoAsync(Guid id);
        Task<string> UpdateTodoAsync(Todo todo);
        Task<(IEnumerable<Todo>, PagingMetaData)> GetTodosAsync(string? name, int PageNumber, int PageSize);
        Task<Todo> GetTodoByIdAsync(Guid id);
    }
}