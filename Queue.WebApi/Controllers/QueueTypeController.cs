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

public class QueueTypeController(ILogger<QueueTypeController> _logger) : BaseController
{
  
    /// <summary>
    /// Получить список всех типов очередей.
    /// </summary>
    /// <returns>Возвращает список типов очередей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationType>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope=new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка типа очереди.");
            var query = new GetQueueTypeListQuery();
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
    /// Получить информацию о конкретном типе очереди.
    /// </summary>
    /// <param name="queueTypeId">Идентификатор типа очереди.</param>
    /// <returns>Возвращает детали типа очереди.</returns>
    [HttpGet("{queueTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueueType))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int queueTypeId)
    {
        var scope = new Dictionary<string, object>() { {"QueueTypeId",queueTypeId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение типа очереди c id");
            var query = new GetQueueTypeByIdQuery(queueTypeId);
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
    /// Создать новый тип очереди.
    /// </summary>
    /// <param name="createQueueTypeDto">Данные нового типа очереди.</param>
    /// <returns>Возвращает идентификатор созданного типа очереди.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Create([FromBody] CreateQueueTypeDto createQueueTypeDto)
    {
        var scope = new Dictionary<string, object>() { { "QueueTypeName",createQueueTypeDto.NameEn} };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание типа очереди");
            var result = await Mediator.Send(Mapper.Map<CreateQueueTypeCommand>(createQueueTypeDto));
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
    /// Обновить информацию о типе очереди.
    /// </summary>
    /// <param name="updateQueueTypeDto">Данные для обновления типа очереди.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateQueueTypeDto updateQueueTypeDto)
    {
        var scope = new Dictionary<string, object>() { { "QueueTypeId", updateQueueTypeDto.QueueTypeId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление типа очереди с id {Id}",updateQueueTypeDto.QueueTypeId);
            var result = await Mediator.Send(Mapper.Map<UpdateQueueTypeCommand>(updateQueueTypeDto));
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
    /// Удалить тип очереди.
    /// </summary>
    /// <param name="id">Идентификатор типа очереди.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { {"QueueTypeId",id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление типа очереди с id {Id}", id);
            var result = await Mediator.Send(new DeleteQueueTypeCommand(id));
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

