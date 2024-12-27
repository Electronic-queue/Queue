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

public class ServiceController : BaseController
{
    private readonly ILogger<ServiceController> _logger;
    /// <summary>
    /// Получить список всех услуг.
    /// </summary>
    /// <returns>Возвращает список услуг.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ServiceListVm>> GetAll()
    {
        var query = new GetServiceListQuery();
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретной услуге.
    /// </summary>
    /// <param name="ServiceId">Идентификатор услуги.</param>
    /// <returns>Возвращает детали услуги.</returns>
    [HttpGet("{ServiceId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ServiceByIdVm>> Get(int ServiceId)
    {
        var query = new GetServiceByIdQuery
        {
            ServiceId = ServiceId
        };
        var vm = await Mediator.Send(query);
        if(vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новую услугу.
    /// </summary>
    /// <param name="createServiceDto">Данные новоой услуги.</param>
    /// <returns>Возвращает идентификатор созданной услуги.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateServiceDto createServiceDto)
    {
        var command = Mapper.Map<CreateServiceCommand>(createServiceDto);
        var serviceId = await Mediator.Send(command);
        if (serviceId.IsFailed)
        {
            return ProblemResponse(serviceId.Error); 
        }
        return Ok(serviceId);

    }
    /// <summary>
    /// Обновить информацию об услуге.
    /// </summary>
    /// <param name="updateServiceDto">Данные для обновления услуги.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateServiceDto updateServiceDto)
    {
        var command = Mapper.Map<UpdateServiceCommand>(updateServiceDto);
        var serviceId = await Mediator.Send(command);
        if (serviceId.IsFailed)
        {
            return ProblemResponse(serviceId.Error);
        }
        return Ok(serviceId);
    }
    /// <summary>
    /// Удалить услугу.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteServiceCommand(id));
        if(command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }
        return NoContent();
    }
}
