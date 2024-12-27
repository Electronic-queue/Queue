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

public class NotificationController : BaseController
{
    private readonly ILogger<NotificationController> _logger;
    /// <summary>
    /// Получить список всех уведомлений.
    /// </summary>
    /// <returns>Возвращает список уведомлений.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<NotificationListVm>> GetAll()
    {
        var query = new GetNotificationListQuery();
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном уведомлении.
    /// </summary>
    /// <param name="notificationId">Идентификатор уведомления.</param>
    /// <returns>Возвращает детали уведомления.</returns>
    [HttpGet("{notificationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<NotificationByIdVm>> Get(int notificationId)
    {
        var query = new GetNotificationByIdQuery
        {
            NotificationId = notificationId
        };
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новое уведомление.
    /// </summary>
    /// <param name="createNotificationDto">Данные нового уведомления.</param>
    /// <returns>Возвращает идентификатор созданного уведомления.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateNotificationDto createNotificationDto)
    {
        var command = Mapper.Map<CreateNotificationCommand>(createNotificationDto);
        var notificationId = await Mediator.Send(command);
        if (notificationId.IsFailed)
        {
            return ProblemResponse(notificationId.Error);
        }
        return Ok(notificationId);

    }
    /// <summary>
    /// Обновить информацию о уведомлении.
    /// </summary>
    /// <param name="updateNotificationDto">Данные для обновления уведомления.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateNotificationDto updateNotificationDto)
    {
        var command = Mapper.Map<UpdateNotificationCommand>(updateNotificationDto);
        var notificationId = await Mediator.Send(command);
        if (notificationId.IsFailed)
        {
            return ProblemResponse(notificationId.Error); ;
        }
        return Ok(notificationId);
    }
    /// <summary>
    /// Удалить уведомление.
    /// </summary>
    /// <param name="id">Идентификатор уведомления.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteNotificationCommand(id));
        if (command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }
        return NoContent();
    }
}
