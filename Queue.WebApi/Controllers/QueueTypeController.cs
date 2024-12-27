using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.QueueTypes.Commands.CreateQueueType;
using Queue.Application.QueueTypes.Commands.DeleteQueueType;
using Queue.Application.QueueTypes.Commands.UpdateQueueType;
using Queue.Application.QueueTypes.Queries.GetQueueTypeById;
using Queue.Application.QueueTypes.Queries.GetQueueTypeList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.QueueTypeContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class QueueTypeController : BaseController
{
    private readonly ILogger<QueueTypeController> _logger;
    /// <summary>
    /// Получить список всех типов очередей.
    /// </summary>
    /// <returns>Возвращает список типов очередей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<QueueType>> GetAll()
    {
        var query = new GetQueueTypeListQuery();
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном типе очереди.
    /// </summary>
    /// <param name="queueTypeId">Идентификатор типа очереди.</param>
    /// <returns>Возвращает детали типа очереди.</returns>
    [HttpGet("{queueTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<Result>> Get(int queueTypeId)
    {
        var query = new GetQueueTypeByIdQuery(queueTypeId);
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новый тип очереди.
    /// </summary>
    /// <param name="createQueueTypeDto">Данные нового типа очереди.</param>
    /// <returns>Возвращает идентификатор созданного типа очереди.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateQueueTypeDto createQueueTypeDto)
    {
        var command = Mapper.Map<CreateQueueTypeCommand>(createQueueTypeDto);
        var queueTypeId = await Mediator.Send(command);
        if (queueTypeId.IsFailed)
        {
            return ProblemResponse(queueTypeId.Error);
        }
        return Ok(queueTypeId);
            
    }
    /// <summary>
    /// Обновить информацию о типе очереди.
    /// </summary>
    /// <param name="updateQueueTypeDto">Данные для обновления типа очереди.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateQueueTypeDto updateQueueTypeDto)
    {
        var command = Mapper.Map<UpdateQueueTypeCommand>(updateQueueTypeDto);
        var queueTypeId = await Mediator.Send(command);
        if (queueTypeId.IsFailed)
        {
            return ProblemResponse(queueTypeId.Error);
        }
        return Ok(queueTypeId);
    }
    /// <summary>
    /// Удалить тип очереди.
    /// </summary>
    /// <param name="id">Идентификатор типа очереди.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteQueueTypeCommand(id));
        if(command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }
        return NoContent();
    }
}

