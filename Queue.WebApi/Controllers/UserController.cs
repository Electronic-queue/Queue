using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.Users.Commands.CreateUser;
using Queue.Application.Users.Commands.DeleteUser;
using Queue.Application.Users.Commands.UpdateUser;
using Queue.Application.Users.Queries.GetUserDetails;
using Queue.Application.Users.Queries.GetUserList;
using Queue.Domain;
using Queue.WebApi.Models;
using System.Net;

namespace Queue.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<UserListVm>> GetAll(Guid id)
        {
            var query = new GetUserListQuery
            {
                Id = id

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
            var command = Mapper.Map<CreateUserCommand>(createUserDto);
            var userId = await Mediator.Send(command);
            return Ok(userId);

        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUsereDto updateUserDto)
        {
            await Mediator.Send(new UpdateUserCommand(updateUserDto.Id,updateUserDto.Iin,updateUserDto.FirstName,updateUserDto.LastName));
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(UpdateUsereDto), (int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = await Mediator.Send(new DeleteUserCommand(id));
            
          
            return NoContent();
        }
    }
}
