using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.RecordStatus.Commands.CreateRecordStatus;
using Queue.Application.RecordStatus.Commands.DeleteRecordStatus;
using Queue.Application.RecordStatus.Commands.UpdateRecordStatus;
using Queue.Application.RecordStatus.Queries.GetRecordStatusById;
using Queue.Application.RecordStatus.Queries.GetRecordStatusList;
using Queue.Application.Windows.Queries.GetWindowDetails;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.RecordStatusContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class RecordStatusController(ILogger<RecordStatusController> _logger) : BaseController
{
    
    /// <summary>
    /// Получить список всех статусов записей
    /// </summary>
    ///
    /// <returns>Возвращает список статусов записей.</returns>
    /// 

    [HttpGet]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RecordStatus>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка типа записей.");
            var query = new GetRecordStatusListQuery();

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
    /// Получить информацию о конкретном статусе записи.
    /// </summary>
    /// <param name="recordStatusId">Идентификатор статуса записи.</param>
    /// <returns>Возвращает детали статуса записи.</returns>
    [HttpGet("{recordStatusId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RecordStatus))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int recordStatusId)
    {
        _logger.LogInformation("Отправка запроса на чтение типа записи с id {Id}.",recordStatusId);
        var scope=new Dictionary<string, object>() { {"RecordStatusId", recordStatusId } };
        using (_logger.BeginScope(scope))
        {
            var query = new GetRecordStatusByIdQuery(recordStatusId);
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
    /// Создать новый статус записи.
    /// </summary>
    /// <param name="createRecordStatusDto">Данные нового статуса записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]


    public async Task<IActionResult> Create([FromBody] CreateRecordStatusDto createRecordStatusDto)
    {
        _logger.LogInformation("Отправка запроса на создание типа записи.");
        var scope = new Dictionary<string, object>() { { "RecordStatusName", createRecordStatusDto.NameEn } };
        using (_logger.BeginScope(scope))
        {
            var result = await Mediator.Send(Mapper.Map<CreateRecordStatusCommand>(createRecordStatusDto));
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
    /// Обновить информацию о статусе записи.
    /// </summary>
    /// <param name="updateRecordStatusDto">Данные для обновления статуса записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]


    public async Task<IActionResult> Update([FromBody] UpdateRecordStatusDto updateRecordStatusDto)
    {
        var scope = new Dictionary<string, object>() { { "RecordStatusId", updateRecordStatusDto.RecordStatusId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление типа записи с id {Id}.", updateRecordStatusDto.RecordStatusId);

            var result = await Mediator.Send(Mapper.Map<UpdateRecordStatusCommand>(updateRecordStatusDto));
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
    /// Удалить статус записи.
    /// </summary>
    /// <param name="id">Идентификатор статуса записи.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "RecordStatusId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление типа записи с id {Id}.", id);

            var result = await Mediator.Send(new DeleteRecordStatusCommand(id));
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
