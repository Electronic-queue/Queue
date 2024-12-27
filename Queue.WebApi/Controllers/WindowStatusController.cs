using Microsoft.AspNetCore.Mvc;
using Queue.Application.WindowStatuses.Commands.CreateWindowStatus;
using Queue.Application.WindowStatuses.Commands.DeletWindowStatus;
using Queue.Application.WindowStatuses.Commands.UpdateWIndowStatus;
using Queue.Application.WindowStatuses.Queries.GetWindowStatusById;
using Queue.Application.WindowStatuses.Queries.GetWindowStatusList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.WindowStatusContracts;
using System.Net;

namespace Queue.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class WindowStatusController : BaseController
{
    private readonly ILogger<WindowStatusController> _logger;
    /// <summary>
    /// Получить список всех окон
    /// </summary>
    ///
    /// <returns>Возвращает список окон.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<WindowStatus>> GetAll()
    {
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        var query = new GetWindowStatusListQuery();

        var vm = await Mediator.Send(query);
        if(vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }

        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном окне.
    /// </summary>
    /// <param name="id">Идентификатор окна.</param>
    /// <returns>Возвращает детали окна.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> Get(int WindowStatusId)
    {
        var query = new GetWindowStatusByIdQuery(WindowStatusId);
        
        var vm = await Mediator.Send(query);
        if(vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новое окно.
    /// </summary>
    /// <param name="createWindowStatusDto">Данные нового окна.</param>
    /// <returns>Возвращает идентификатор созданного окна.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateWindowStatusDto createWindowStatusDto)
    {
        var command = Mapper.Map<CreateWindowStatusCommand>(createWindowStatusDto);
        var windowStatusId = await Mediator.Send(command);
        if (windowStatusId.IsFailed)
        {
            return ProblemResponse(windowStatusId.Error);
        }
        return Ok(windowStatusId);

    }
    /// <summary>
    /// Обновить информацию об окне.
    /// </summary>
    /// <param name="updateWindowStatusDto">Данные для обновления окна.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateWindowStatusDto updateWindowStatusDto)
    {
        var command = Mapper.Map<UpdateWindowStatusCommand>(updateWindowStatusDto);
        var windowStatusId = await Mediator.Send(command);
        if (windowStatusId.IsFailed)
        {
            return ProblemResponse(windowStatusId.Error);
        }
        return Ok(windowStatusId);
    }
    /// <summary>
    /// Удалить окно.
    /// </summary>
    /// <param name="id">Идентификатор окна.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteWindowStatusCommand(id));
        if(command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }

        return NoContent();
    }
}

