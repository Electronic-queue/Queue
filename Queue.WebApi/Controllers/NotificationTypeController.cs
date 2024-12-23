using Microsoft.AspNetCore.Mvc;
using Queue.Application.NotificationTypes.Commands.CreateNotificationType;
using Queue.Application.NotificationTypes.Commands.DeleteNotificationType;
using Queue.Application.NotificationTypes.Commands.UpdateNotificationType;
using Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;
using Queue.Application.NotificationTypes.Queries.GetNotificationTypeList;
using Queue.WebApi.Contracts.NotificationTypeContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class NotificationTypeController : BaseController
{
    private readonly ILogger<NotificationTypeController> _logger;
    /// <summary>
    /// Получить список всех типов уведомлений.
    /// </summary>
    /// <returns>Возвращает список типов уведомлений.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<NotificationTypeListVm>> GetAll()
    {
        var query = new GetNotificationTypeListQuery();
        var vm = await Mediator.Send(query);
        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном типе уведомлений.
    /// </summary>
    /// <param name="notificationTypeId">Идентификатор типа уведомлений.</param>
    /// <returns>Возвращает детали типа уведомлений.</returns>
    [HttpGet("{notificationTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<NotificationTypeDetailsVm>> Get(int notificationTypeId)
    {
        var query = new GetNotificationTypeDetailsQuery
        {
            NotificationTypeId = notificationTypeId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Создать новый тип уведомлений.
    /// </summary>
    /// <param name="createNotificationTypeDto">Данные нового типа уведомлений.</param>
    /// <returns>Возвращает идентификатор созданного типа уведомлений.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateNotificationTypeDto createNotificationTypeDto)
    {
        var command = Mapper.Map<CreateNotificationTypeCommand>(createNotificationTypeDto);
        var notificationTypeId = await Mediator.Send(command);
        if (notificationTypeId.IsFailed)
        {
            return BadRequest(notificationTypeId);
        }
        return Ok(notificationTypeId);

    }
    /// <summary>
    /// Обновить информацию о типе уведомлений.
    /// </summary>
    /// <param name="updateNotificationTypeDto">Данные для обновления типа уведомлений.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateNotificationTypeDto updateNotificationTypeDto)
    {
        var command = Mapper.Map<UpdateNotificationTypeCommand>(updateNotificationTypeDto);
        var notificationTypeId = await Mediator.Send(command);
        if (notificationTypeId.IsFailed)
        {
            return BadRequest(notificationTypeId);
        }
        return Ok(notificationTypeId);
    }
    /// <summary>
    /// Удалить тип уведомлений.
    /// </summary>
    /// <param name="id">Идентификатор типа уведомлений.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteNotificationTypeCommand(id));
        return NoContent();
    }
}
