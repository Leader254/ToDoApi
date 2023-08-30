using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Requests;
using TodoApp.Responses;
using TodoApp.Services.IService;

namespace TodoApp.Controllers
{
    // use UserResponse for any response we are expecting from the server (GET)
    // use UserSuccess form anyrhing we are sending to the server (POST, PUT, DELETE)
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        // add a user done
        [HttpPost]
        public async Task<ActionResult<UserSuccess>> AddUser(AddUser user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);
                var success = await _userService.AddUserAsync(newUser);
                return CreatedAtAction(nameof(AddUser), new UserSuccess(success, 201));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Something went wrong", 400));
            }
        }

        // update user done
        [HttpPut("{id}")]
        public async Task<ActionResult<UserSuccess>> UpdateUser(Guid id, AddUser newUser)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            var updatedUser = _mapper.Map(newUser, user);
            try
            {
                var success = await _userService.UpdateUserAsync(updatedUser);
                return Ok(new UserSuccess(success, 200));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Something went wrong", 400));
            }
        }

        // delete user done
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserSuccess>> DeleteUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            try
            {
                var success = await _userService.DeleteUserAsync(id);
                return Ok(new UserSuccess(success, 200));
            }
            catch (Exception)
            {
                return BadRequest(new UserSuccess("Something went wrong", 400));
            }
        }

        // get user by id done
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoUser>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(new UserSuccess("User not found", 404));
            }
            return Ok(user);
        }

        // get all users done
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoUser>>> GetAllUsers()
        {
            var users = await _userService.GetUsersAsync();
            if (users == null)
            {
                return NotFound(new UserSuccess("Users not found", 404));
            }
            return Ok(users);
        }
    }
}