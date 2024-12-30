using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.NotificationTypes.Commands.CreateNotificationType;
using Queue.Application.NotificationTypes.Commands.DeleteNotificationType;
using Queue.Application.NotificationTypes.Commands.UpdateNotificationType;
using Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;
using Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.NotificationTypeContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class NotificationTypeController(ILogger<NotificationTypeController> _logger) : BaseController
{
   
    /// <summary>
    /// Получить список всех типов уведомлений.
    /// </summary>
    /// <returns>Возвращает список типов уведомлений.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationType>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope= new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка типа уведомлений.");
            var query = new GetNotificationTypeListQuery();
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
    /// Получить информацию о конкретном типе уведомлений.
    /// </summary>
    /// <param name="notificationTypeId">Идентификатор типа уведомлений.</param>
    /// <returns>Возвращает детали типа уведомлений.</returns>
    [HttpGet("{notificationTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(NotificationType))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int notificationTypeId)
    {
        var scope=new Dictionary<string, object>() { { "NotificationTypeId",notificationTypeId} };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение типа уведомлений c id");
            var query = new GetNotificationTypeDetailsQuery(notificationTypeId);
           
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
    /// Создать новый тип уведомлений.
    /// </summary>
    /// <param name="createNotificationTypeDto">Данные нового типа уведомлений.</param>
    /// <returns>Возвращает идентификатор созданного типа уведомлений.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Create([FromBody] CreateNotificationTypeDto createNotificationTypeDto)
    {

        var scope = new Dictionary<string, object>() { {"NotificationTypeName",createNotificationTypeDto.NameEn } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание типа уведомления");
            var result = await Mediator.Send(Mapper.Map<CreateNotificationTypeCommand>(createNotificationTypeDto));

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
    /// Обновить информацию о типе уведомлений.
    /// </summary>
    /// <param name="updateNotificationTypeDto">Данные для обновления типа уведомлений.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Update([FromBody] UpdateNotificationTypeDto updateNotificationTypeDto)
    {
        var scope = new Dictionary<string, object>() { {"NotificationTypeId",updateNotificationTypeDto.NotificationTypeId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление типа уведомления с id {Id}",updateNotificationTypeDto.NotificationTypeId);
            var result = await Mediator.Send(Mapper.Map<UpdateNotificationTypeCommand>(updateNotificationTypeDto));
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
    /// Удалить тип уведомлений.
    /// </summary>
    /// <param name="id">Идентификатор типа уведомлений.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "NotificationTypeId",id} };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление типа уведомления с id {Id}", id);
            var result = await Mediator.Send(new DeleteNotificationTypeCommand(id));
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
