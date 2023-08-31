using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Requests;
using TodoApp.Responses;
using TodoApp.Services.IService;

namespace TodoApp.Controllers
{
    // use TodoResponse for any response we are expecting from the server (GET)
    // use UserSuccess form anyrhing we are sending to the server (POST, PUT, DELETE)
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITodoService _todoService;
        const int maxPageSize = 5;

        public TodoController(IMapper mapper, ITodoService todoService)
        {
            _mapper = mapper;
            _todoService = todoService;
        }

        [HttpPost]
        // add a todo - use success
        public async Task<ActionResult<UserSuccess>> AddTodo(AddTodo todo)
        {
            try
            {
                var newTodo = _mapper.Map<Todo>(todo);
                var success = await _todoService.AddTodoAsync(newTodo);
                return CreatedAtAction(nameof(AddTodo), new UserSuccess(success, 201));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Something went wrong", 400));
            }
        }

        [HttpGet]
        // get all todos - use response
        public async Task<ActionResult<IEnumerable<TodoResponse>>> GetAllTodos(string? name, int PageNumber = 1, int PageSize = 1)
        {
            if (PageSize > maxPageSize)
            {
                PageSize = maxPageSize;
            }
            var (todos, meta) = await _todoService.GetTodosAsync(name, PageNumber, PageSize);
            Response.Headers.Add("PagingMetaData", JsonSerializer.Serialize(meta));
            var response = _mapper.Map<IEnumerable<TodoResponse>>(todos);
            return Ok(response);
        }

        [HttpGet("{id}")]
        // get todo by id - use response
        public async Task<ActionResult<TodoResponse>> GetSingleTodoById(Guid id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new UserSuccess("Todo not found", 404));
            }
            var response = _mapper.Map<TodoResponse>(todo);
            return Ok(response);
        }

        [HttpPut("{id}")]
        // update todo - use success
        public async Task<ActionResult<UserSuccess>> UpdateTodoById(Guid id, UpdateTodo todo)
        {
            var todo1 = await _todoService.GetTodoByIdAsync(id);
            if (todo1 == null)
            {
                return NotFound(new UserSuccess("Todo not found", 404));
            }
            var updatedTodo = _mapper.Map(todo, todo1);
            var res = await _todoService.UpdateTodoAsync(updatedTodo);
            return Ok(new UserSuccess(res, 200));
        }

        [HttpDelete("{id}")]
        // delete todo - use success
        public async Task<ActionResult<UserSuccess>> DeleteTodoById(Guid id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound(new UserSuccess("Todo not found", 404));
            }
            var res = await _todoService.DeleteTodoAsync(id);
            return Ok(new UserSuccess(res, 200));
        }
    }
}