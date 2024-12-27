using Microsoft.AspNetCore.Mvc;
using Queue.Application.UserServices.Commands.CreateUserService;
using Queue.Application.UserServices.Commands.DeleteUserService;
using Queue.Application.UserServices.Commands.UpdateUserService;
using Queue.Application.UserServices.Queries.GetUserServiceById;
using Queue.Application.UserServices.Queries.GetUserServiceList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.UserServiceContracts;
using System.Net;

namespace Queue.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class UserServiceController : BaseController
{
    private readonly ILogger<UserServiceController> _logger;
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Возвращает список услуг.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<List<UserService>>> GetAll()
    {
        var query = new GetUserServiceListQuery();
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретной услуге.
    /// </summary>
    /// <param name="userServiceId">Идентификатор услуги.</param>
    /// <returns>Возвращает детали услуги.</returns>
    [HttpGet("{userServiceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<UserService>> Get(int userServiceId)
    {
        var query = new GetUserServiceByIdQuery(userServiceId);
        var vm = await Mediator.Send(query);
        if(vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    /// <param name="createUserServiceDto">Данные новоой услуги.</param>
    /// <returns>Возвращает идентификатор созданной услуги.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateUserServiceDto createUserServiceDto)
    {
        var command = Mapper.Map<CreateUserServiceCommand>(createUserServiceDto);
        var userServiceId = await Mediator.Send(command);
        if (userServiceId.IsFailed)
        {
            return ProblemResponse(userServiceId.Error);
        }
        return Ok(userServiceId);

    }
    /// <summary>
    /// Обновить информацию об услуге.
    /// </summary>
    /// <param name="updateUserServiceDto">Данные для обновления услуги.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateUserServiceDto updateUserServiceDto)
    {
        var command = Mapper.Map<UpdateUserServiceCommand>(updateUserServiceDto);
        var userServiceId = await Mediator.Send(command);
        if (userServiceId.IsFailed)
        {
            return ProblemResponse(userServiceId.Error);
        }
        return Ok(userServiceId);
    }
    /// <summary>
    /// Удалить услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteUserServiceCommand(id));
        if (command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }
        return NoContent();
    }
}
