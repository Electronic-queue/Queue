using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.Users.Commands.CreateUser;
using Queue.Application.Users.Commands.DeleteUser;
using Queue.Application.Users.Commands.UpdateUser;
using Queue.Application.Users.Queries.GetUserDetails;
using Queue.Application.Users.Queries.GetUserList;
using Queue.Domain;
using Queue.WebApi.Models;

namespace Queue.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;
        public UserController(IMapper mapper) => _mapper = mapper;
        [HttpGet]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetUserListQuery
            {
                Id = Id

            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailsVm>> Get(Guid id)
        {
            var query = new GetUserDetailsQuery
            {
                
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            command.Id = Id;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUsereDto updateUserDto)
        {
            var command = _mapper.Map<UpdateUserCommand>(updateUserDto);
            command.Id = Id;
            await Mediator.Send(command);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand
            {
                Id = id
               
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
