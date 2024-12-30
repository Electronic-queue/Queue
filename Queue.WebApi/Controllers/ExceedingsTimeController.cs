using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.ExceedingsTimes.Commands.CreateExceedingsTime;
using Queue.Application.ExceedingsTimes.Commands.DeleteExceedingsTime;
using Queue.Application.ExceedingsTimes.Commands.UpdateExceedingsTime;
using Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeById;
using Queue.Application.ExceedingsTimes.Queries.GetExceedingsTimeList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.ExceedingsTimeContracts;
using System.Net;

namespace Queue.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class ExceedingsTimeController(ILogger<ExceedingsTimeController> _logger) : BaseController
{

    /// <summary>
    /// Получить список всех времени перерывов
    /// </summary>
    ///
    /// <returns>Возвращает список  времени перерывов.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(List<ExceedingsTime>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type=typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка времени перерывов.");
            var query = new GetExceedingsTimeListQuery();

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
    /// Получить информацию о конкретном времени перерыва.
    /// </summary>
    /// <param name="exceedingsTimeId">Идентификатор  времени перерыва.</param>
    /// <returns>Возвращает детали  времени перерыва.</returns>
    [HttpGet("{exceedingsTimeId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExceedingsTime))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int exceedingsTimeId)
    {
        var scope = new Dictionary<string, object>() { { "exceedingsTimeId" , exceedingsTimeId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение врменеи перерыва с id");
            var query = new GetExceedingsTimeByIdQuery(exceedingsTimeId);
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
    /// Создать новый временный перерыв.
    /// </summary>
    /// <param name="createExceedingsTimeDto">Данные нового врменного перерыва.</param>
    /// <returns>Возвращает идентификатор созданного времени перерыва.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateExceedingsTimeDto createExceedingsTimeDto)
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание врменеи перерыва ");
            var result = await Mediator.Send(Mapper.Map<CreateExceedingsTimeCommand>(createExceedingsTimeDto));
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
    /// Обновить информацию о  времени перерыва.
    /// </summary>
    /// <param name="updateExceedingsTimeDto">Данные для обновления  времени перерыва.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Update([FromBody] UpdateExceedingsTimeDto updateExceedingsTimeDto)
    {
        var scope=new Dictionary<string, object>() { {"ExceedingsTimeId",updateExceedingsTimeDto.ExceedingsTimeId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление врменеи перерыва с id {Id}",updateExceedingsTimeDto.ExceedingsTimeId);
            var result = await Mediator.Send(Mapper.Map<UpdateExceedingsTimeCommand>(updateExceedingsTimeDto));
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
    /// Удалить время перерыва.
    /// </summary>
    /// <param name="id">Идентификатор  времени перерыва.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope=new Dictionary<string, object>() { { "ExceedingsTimeId",id} };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление врменеи перерыва с id {Id}", id);
            var result = await Mediator.Send(new DeleteExceedingsTimeCommand(id));

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



