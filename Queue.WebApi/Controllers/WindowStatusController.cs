using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.WindowStatuses.Commands.CreateWindowStatus;
using Queue.Application.WindowStatuses.Commands.DeleteWindowStatus;
using Queue.Application.WindowStatuses.Commands.UpdateWIndowStatus;
using Queue.Application.WindowStatuses.Queries.GetWindowStatusById;
using Queue.Application.WindowStatuses.Queries.GetWindowStatusList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.WindowStatusContracts;

namespace Queue.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class WindowStatusController(ILogger<WindowStatusController> _logger) : BaseController
{

    /// <summary>
    /// Получить список всех окон
    /// </summary>
    ///
    /// <returns>Возвращает список окон.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WindowStatus>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()

    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка статуса окон.");
            var query = new GetWindowStatusListQuery();

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
    /// Получить информацию о конкретном окне.
    /// </summary>
    /// <param name="windowStatusId">Идентификатор окна.</param>
    /// <returns>Возвращает детали окна.</returns>
    [HttpGet("{windowStatusId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WindowStatus))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int windowStatusId)
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение статуса окна с id {Id}", windowStatusId);
            var query = new GetWindowStatusByIdQuery(windowStatusId);

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
    /// Создать новое окно.
    /// </summary>
    /// <param name="createWindowStatusDto">Данные нового окна.</param>
    /// <returns>Возвращает идентификатор созданного окна.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateWindowStatusDto createWindowStatusDto)
    {
        var scope = new Dictionary<string, object>() { { "WindowStatusName", createWindowStatusDto.NameEn } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание статуса окна");
            var result = await Mediator.Send(Mapper.Map<CreateWindowStatusCommand>(createWindowStatusDto));
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
    /// Обновить информацию об окне.
    /// </summary>
    /// <param name="updateWindowStatusDto">Данные для обновления окна.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Update([FromBody] UpdateWindowStatusDto updateWindowStatusDto)
    {
        var scope = new Dictionary<string, object>() { { "WindowStatusId", updateWindowStatusDto.WindowStatusId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление окна с id {Id}", updateWindowStatusDto.WindowStatusId);
            var result = await Mediator.Send(Mapper.Map<UpdateWindowStatusCommand>(updateWindowStatusDto));
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
    /// Удалить окно.
    /// </summary>
    /// <param name="id">Идентификатор окна.</param>
    /// 
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "WindowStatusId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление статуса окна с id {Id}", id);
            var result = await Mediator.Send(new DeleteWindowStatusCommand(id));
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

