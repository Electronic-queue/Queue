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

public class ExceedingsTimeController : BaseController
{
    private readonly ILogger<ExceedingsTimeController> _logger;
    /// <summary>
    /// Получить список всех записей
    /// </summary>
    ///
    /// <returns>Возвращает список  записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<List<ExceedingsTime>>> GetAll()
    {


        var query = new GetExceedingsTimeListQuery();

        var vm = await Mediator.Send(query);
        if (vm.IsFailed) {
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
    public async Task<ActionResult<ExceedingsTime>> Get(int ExceedingsTimeId)
    {
        var query = new GetExceedingsTimeByIdQuery(ExceedingsTimeId);
        var vm = await Mediator.Send(query);
        if(vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новый  записи.
    /// </summary>
    /// <param name="createExceedingsTimeDto">Данные новой записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateExceedingsTimeDto createExceedingsTimeDto)
    {
        var command = Mapper.Map<CreateExceedingsTimeCommand>(createExceedingsTimeDto);
        var exceedingsTimeId = await Mediator.Send(command);
        if (exceedingsTimeId.IsFailed)
        {
            return ProblemResponse(exceedingsTimeId.Error);
        }
        return Ok(exceedingsTimeId);

    }
    /// <summary>
    /// Обновить информацию о  записи.
    /// </summary>
    /// <param name="updateExceedingsTimeDto">Данные для обновления  записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateExceedingsTimeDto updateExceedingsTimeDto)
    {
        var command = Mapper.Map<UpdateExceedingsTimeCommand>(updateExceedingsTimeDto);
        var exceedingsTimeId = await Mediator.Send(command);
        if (exceedingsTimeId.IsFailed)
        {
            return ProblemResponse(exceedingsTimeId.Error);
        }
        return Ok(exceedingsTimeId);
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
        var command = await Mediator.Send(new DeleteExceedingsTimeCommand(id));

        if (command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }
        return NoContent();
    }
}



