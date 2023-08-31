using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Context;
using TodoApp.Models;
using TodoApp.Responses;
using TodoApp.Services.IService;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext context;
        public TodoService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> AddTodoAsync(Todo todo)
        {
            context.Todos.Add(todo);
            await context.SaveChangesAsync();
            return "Todo added successfully";
        }

        public async Task<string> DeleteTodoAsync(Guid id)
        {
            var todo = await context.Todos.FindAsync(id);
            if (todo == null)
            {
                return "Todo not found";
            }
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return "Todo deleted successfully";
        }

        public async Task<Todo> GetTodoByIdAsync(Guid id)
        {
            var result = await context.Todos.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<(IEnumerable<Todo>, PagingMetaData)> GetTodosAsync(string? name, int PageNumber, int PageSize)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
            {
                var Count = await context.Todos.CountAsync();
                var Meta = new PagingMetaData(PageNumber, PageSize, Count);
                var response = await context.Todos.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();
                return (response, Meta);
            }
            var TotalCount = await context.Todos.CountAsync();
            var pageMeta = new PagingMetaData(PageNumber, PageSize, TotalCount);
            // searching and filtering
            var res = await context.Todos.Where(c => c.Name.ToLower().Contains(name.ToLower())).Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();
            return (res, pageMeta);
        }

        public async Task<string> UpdateTodoAsync(Todo todo)
        {
            context.Todos.Update(todo);
            await context.SaveChangesAsync();
            return "Todo updated successfully";
        }
    }
}