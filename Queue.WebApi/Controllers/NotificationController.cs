using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.Notifications.Commands.CreateNotification;
using Queue.Application.Notifications.Commands.DeleteNotification;
using Queue.Application.Notifications.Commands.UpdateNotification;
using Queue.Application.Notifications.Queries.GetNotificationById;
using Queue.Application.Notifications.Queries.GetNotificationList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.NotificationContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class NotificationController(ILogger<NotificationController> _logger) : BaseController
{

    /// <summary>
    /// Получить список всех уведомлений.
    /// </summary>
    /// <returns>Возвращает список уведомлений.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка уведомлении.");
            var query = new GetNotificationListQuery();
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
    /// Получить информацию о конкретном уведомлении.
    /// </summary>
    /// <param name="notificationId">Идентификатор уведомления.</param>
    /// <returns>Возвращает детали уведомления.</returns>
    [HttpGet("{notificationId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Notification))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int notificationId)
    {
        var scope = new Dictionary<string, object>() { { "NotificationId", notificationId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение  уведомления с id.");
            var query = new GetNotificationByIdQuery(notificationId);

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
    /// Создать новое уведомление.
    /// </summary>
    /// <param name="createNotificationDto">Данные нового уведомления.</param>
    /// <returns>Возвращает идентификатор созданного уведомления.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Create([FromBody] CreateNotificationDto createNotificationDto)
    {

        var scope = new Dictionary<string, object>() { { "NotficationName", createNotificationDto.NameEn } };

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание  уведомления.");
            var result = await Mediator.Send(Mapper.Map<CreateNotificationCommand>(createNotificationDto));
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
    /// Обновить информацию о уведомлении.
    /// </summary>
    /// <param name="updateNotificationDto">Данные для обновления уведомления.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateNotificationDto updateNotificationDto)
    {
        var scope = new Dictionary<string, object>() { { "NotificationId", updateNotificationDto.NotificationId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление уведомления с id {Id}", updateNotificationDto.NotificationId);
            var command = Mapper.Map<UpdateNotificationCommand>(updateNotificationDto);
            var result = await Mediator.Send(command);
            if (result.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(result.Error); ;
            }
            _logger.LogInformation("Запрос прошел успешно.");
            return Ok(result);
        }
    }
    /// <summary>
    /// Удалить уведомление.
    /// </summary>
    /// <param name="id">Идентификатор уведомления.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "NotificationId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление  уведомления с id {Id}", id);
            var result = await Mediator.Send(new DeleteNotificationCommand(id));
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
