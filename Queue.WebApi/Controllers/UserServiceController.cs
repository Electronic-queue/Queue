using KDS.Primitives.FluentResult;
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

public class UserServiceController(ILogger<UserServiceController> _logger) : BaseController
{
    
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Возвращает список услуг.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserService>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope=new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка userService.");
            var query = new GetUserServiceListQuery();
            var result = await Mediator.Send(query);
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
            //return ResultSucces.Success(vm);
        }
    }

    /// <summary>
    /// Получить информацию о конкретной услуге.
    /// </summary>
    /// <param name="userServiceId">Идентификатор услуги.</param>
    /// <returns>Возвращает детали услуги.</returns>
    /// 
    [HttpGet("{userServiceId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserService))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int userServiceId)
    {
        var scope=new Dictionary<string, object>() { {"UserSeriveId",userServiceId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение userService с id {Id}",userServiceId);
            var query = new GetUserServiceByIdQuery(userServiceId);
            var result = await Mediator.Send(query);
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    /// <param name="createUserServiceDto">Данные новоой услуги.</param>
    /// <returns>Возвращает идентификатор созданной услуги.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Create([FromBody] CreateUserServiceDto createUserServiceDto)
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание userService");
            var result = await Mediator.Send(Mapper.Map<CreateUserServiceCommand>(createUserServiceDto));
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }

    }
    /// <summary>
    /// Обновить информацию об услуге.
    /// </summary>
    /// <param name="updateUserServiceDto">Данные для обновления услуги.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Update([FromBody] UpdateUserServiceDto updateUserServiceDto)
    {
        var scope=new Dictionary<string, object>() { {"UserServiceId",updateUserServiceDto.UserServiceId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление userService с id {Id}", updateUserServiceDto.UserServiceId);
            var result = await Mediator.Send(Mapper.Map<UpdateUserServiceCommand>(updateUserServiceDto));
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }
    /// <summary>
    /// Удалить услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "UserServiceId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление userService с id {Id}", id);
            var result = await Mediator.Send(new DeleteUserServiceCommand(id));

            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error);
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return NoContent();
        }
    }
}
