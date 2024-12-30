using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.ReasonsForCancellations.Commands.CreateReasonsForCancellation;
using Queue.Application.ReasonsForCancellations.Commands.DeleteReasonsForCancellation;
using Queue.Application.ReasonsForCancellations.Commands.UpdateReasonsForCancellation;
using Queue.Application.ReasonsForCancellations.Queries.GetReasonsForCancellationList;
using Queue.Application.ReasonsForCancellations.Queries.ReasonsForCancellationById;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.ReasonsForCancellationContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class ReasonsForCancellationController(ILogger<ReasonsForCancellationController> _logger) : BaseController
{

    /// <summary>
    /// Получить список всех записей
    /// </summary>
    ///
    /// <returns>Возвращает список  записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotificationType>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {

        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка причин для отмены.");
            var query = new GetReasonsForCancellationListQuery();

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
    /// Получить информацию о конкретной записи.
    /// </summary>
    /// <param name="reasonId">Идентификатор  записи.</param>
    /// <returns>Возвращает детали  записи.</returns>
    [HttpGet("{reasonId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueueType))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int reasonId)
    {
        var scope = new Dictionary<string, object>() { { "ReasonsForCancellationId", reasonId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение причины для отмены с id");
            var query = new GetReasonsForCancellationByIdQuery(reasonId);
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
    /// Создать новый  записи.
    /// </summary>
    /// <param name="createReasonsForCancellationDto">Данные новой записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]


    public async Task<IActionResult> Create([FromBody] CreateReasonsForCancellationDto createReasonsForCancellationDto)
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание причины для отмены");
            var result = await Mediator.Send(Mapper.Map<CreateReasonsForCancellationCommand>(createReasonsForCancellationDto));
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
    /// Обновить информацию о  записи.
    /// </summary>
    /// <param name="updateReasonsForCancellationDto">Данные для обновления  записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Update([FromBody] UpdateReasonsForCancellationDto updateReasonsForCancellationDto)
    {
        var scope = new Dictionary<string, object>() { { "ReasonsForCancellationId", updateReasonsForCancellationDto.ReasonId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление причины для отмены c id {Id}",updateReasonsForCancellationDto.ReasonId);
            var result = await Mediator.Send(Mapper.Map<UpdateReasonsForCancellationCommand>(updateReasonsForCancellationDto));
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
    /// Удалить  записи.
    /// </summary>
    /// <param name="id">Идентификатор  записи.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "ReasonsForCancellationId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление причины для отмены с id {Id}",id);
            var command = await Mediator.Send(new DeleteReasonsForCancellationCommand(id));
            if (command.IsFailed)
            {
                _logger.LogError("Запрос вернул ошибку [{ErrorCode}] [{ErrorMessage}].", result.Error.Code, result.Error.Message);
                return ProblemResponse(command.Error);
            }
        }
        _logger.LogInformation("Запрос прошел успешно.");
        return NoContent();
    }
}



