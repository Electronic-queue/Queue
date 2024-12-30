using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.Services.Commands.CreateService;
using Queue.Application.Services.Commands.DeleteService;
using Queue.Application.Services.Commands.UpdateService;
using Queue.Application.Services.Queries.GetServiceById;
using Queue.Application.Services.Queries.GetServiceList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.ServiceContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class ServiceController(ILogger<ServiceController> _logger) : BaseController
{
   
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Возвращает список услуг.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Service>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope=new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка услуг.");
            var query = new GetServiceListQuery();
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
    /// Получить информацию о конкретной услуге.
    /// </summary>
    /// <param name="serviceId">Идентификатор услуги.</param>
    /// <returns>Возвращает детали услуги.</returns>
    [HttpGet("{serviceId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Service))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int serviceId)
    {
        var scope=new Dictionary<string, object>() { {"ServiceId",serviceId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение услуги с id {Id}.",serviceId);
            var query = new GetServiceByIdQuery(serviceId);

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
    /// Создать новую услугу.
    /// </summary>
    /// <param name="createServiceDto">Данные новоой услуги.</param>
    /// <returns>Возвращает идентификатор созданной услуги.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<ActionResult<int>> Create([FromBody] CreateServiceDto createServiceDto)
    {
        var scope=new Dictionary<string, object>() { {"ServiceName",createServiceDto.NameEn } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание услуги.");
       
            var result = await Mediator.Send(Mapper.Map<CreateServiceCommand>(createServiceDto));
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
    /// Обновить информацию об услуге.
    /// </summary>
    /// <param name="updateServiceDto">Данные для обновления услуги.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Update([FromBody] UpdateServiceDto updateServiceDto)
    {
        var scope = new Dictionary<string, object>() { { "ServiceId",updateServiceDto.ServiceId} };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление услуги с id {Id}.", updateServiceDto.ServiceId);
            var result = await Mediator.Send(Mapper.Map<UpdateServiceCommand>(updateServiceDto));
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
    /// Удалить услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope=new Dictionary<string, object>() { {"ServiceId",id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление услуги с id {Id}.",id);
            var result = await Mediator.Send(new DeleteServiceCommand(id));
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
