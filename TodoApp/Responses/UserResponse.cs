using TodoApp.Models;

namespace TodoApp.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public List<Todo> Todos { get; set; } = new List<Todo>();

    }
}