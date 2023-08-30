using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.Requests
{
    public class AddUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Occupation { get; set; } = "Software Engineer";

    }
}