using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TodoApp.Models;
using TodoApp.Requests;
using TodoApp.Responses;

namespace TodoApp.Profiles
{
    public class TodoProfiles : Profile
    {
        public TodoProfiles()
        {
            // create a user profile
            CreateMap<AddUser, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();

            // create a todo profile
            CreateMap<AddTodo, Todo>().ReverseMap();
            CreateMap<UpdateTodo, Todo>().ReverseMap();
            CreateMap<TodoResponse, Todo>().ReverseMap();
        }
    }
}