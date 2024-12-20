using AutoMapper;
using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.Users.Commands.CreateUser;
using Queue.Application.Users.Commands.DeleteUser;
using Queue.Application.Users.Commands.UpdateUser;
using Queue.Application.Users.Queries.GetUserDetails;
using Queue.Application.Users.Queries.GetUserList;
using Queue.Domain;
using Queue.WebApi.Models;
using System.Net;

namespace Queue.WebApi.Controllers;  

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;
    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <param name="id">Идентификатор пользователя (опционально).</param>
    /// <returns>Возвращает список пользователей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<UserListVm>> GetAll(int UserId)
    {
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();
        _logger.LogInformation("CorrelationId: {CorrelationId} - Получение списка пользователей.", correlationId);
        var query = new GetUserListQuery
        {
            UserId = UserId

        };
        var vm = await Mediator.Send(query);
        _logger.LogInformation("CorrelationId: {CorrelationId} - Данные о пользователе с ID {UserId} получены.", correlationId, UserId);
            return Ok(vm);
            //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном пользователе.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Возвращает детали пользователя.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<UserDetailsVm>> Get(int UserId)
    {
        var query = new GetUserDetailsQuery
        {

            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Создать нового пользователя.
    /// </summary>
    /// <param name="createUserDto">Данные нового пользователя.</param>
    /// <returns>Возвращает идентификатор созданного пользователя.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateUserDto createUserDto)
    {
        var command = Mapper.Map<CreateUserCommand>(createUserDto);
        var userId = await Mediator.Send(command);
            if (userId.IsFailed)
            {
                return BadRequest(userId);
            }
        return Ok(userId);

    }
    /// <summary>
    /// Обновить информацию о пользователе.
    /// </summary>
    /// <param name="updateUserDto">Данные для обновления пользователя.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateUsereDto updateUserDto)
    {
        await Mediator.Send(new UpdateUserCommand(updateUserDto.UserId,updateUserDto.FirstName,updateUserDto.LastName,updateUserDto.Surname,updateUserDto.Login, updateUserDto.PasswordHash, updateUserDto.IsDeleted));
        return NoContent();
    }
    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteUserCommand(id));
        
      
        return NoContent();
    }
}
