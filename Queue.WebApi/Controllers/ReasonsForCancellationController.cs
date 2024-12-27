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

public class ReasonsForCancellationController : BaseController
{
    private readonly ILogger<ReasonsForCancellationController> _logger;
    /// <summary>
    /// Получить список всех записей
    /// </summary>
    ///
    /// <returns>Возвращает список  записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ReasonsForCancellation>> GetAll()
    {


        var query = new GetReasonsForCancellationListQuery();

        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }

        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретной записи.
    /// </summary>
    /// <param name="id">Идентификатор  записи.</param>
    /// <returns>Возвращает детали  записи.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<ReasonsForCancellation>> Get(int ReasonId)
    {
        var query = new GetReasonsForCancellationByIdQuery(ReasonId);
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новый  записи.
    /// </summary>
    /// <param name="createReasonsForCancellationDto">Данные новой записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateReasonsForCancellationDto createReasonsForCancellationDto)
    {
        var command = Mapper.Map<CreateReasonsForCancellationCommand>(createReasonsForCancellationDto);
        var reasonId = await Mediator.Send(command);
        if (reasonId.IsFailed)
        {
            return ProblemResponse(reasonId.Error);
        }
        return Ok(reasonId);

    }
    /// <summary>
    /// Обновить информацию о  записи.
    /// </summary>
    /// <param name="updateReasonsForCancellationDto">Данные для обновления  записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateReasonsForCancellationDto updateReasonsForCancellationDto)
    {
        var command = Mapper.Map<UpdateReasonsForCancellationCommand>(updateReasonsForCancellationDto);
        var reasonId = await Mediator.Send(command);
        if (reasonId.IsFailed)
        {
            return ProblemResponse(reasonId.Error);
        }
        return Ok(reasonId);
    }
    /// <summary>
    /// Удалить  записи.
    /// </summary>
    /// <param name="id">Идентификатор  записи.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteReasonsForCancellationCommand(id));
        if(command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }

        return NoContent();
    }
}



