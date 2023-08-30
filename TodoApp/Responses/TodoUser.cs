using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Responses
{
    public class TodoUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Occupation { get; set; } = string.Empty;
        public List<TodoResponseDto> Todos { get; set; } = new List<TodoResponseDto>();
    }

    public class TodoResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsComplete { get; set; }
    }
}