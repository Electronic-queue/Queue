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

public class RecordStatusController : BaseController
{
    private readonly ILogger<WindowController> _logger;
    /// <summary>
    /// Получить список всех статусов записей
    /// </summary>
    ///
    /// <returns>Возвращает список статусов записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<RecordStatus>> GetAll()
    {
        var query = new GetRecordStatusListQuery();

        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }

        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном статусе записи.
    /// </summary>
    /// <param name="id">Идентификатор статуса записи.</param>
    /// <returns>Возвращает детали статуса записи.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<WindowDetailsVm>> Get(int RecordStatusId)
    {
        var query = new GetRecordStatusByIdQuery(RecordStatusId);
        var vm = await Mediator.Send(query);
        if (vm.IsFailed)
        {
            return ProblemResponse(vm.Error);
        }
        return Ok(vm);
    }

    /// <summary>
    /// Создать новый статус записи.
    /// </summary>
    /// <param name="createRecordStatusDto">Данные нового статуса записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateRecordStatusDto createRecordStatusDto)
    {
        var command = Mapper.Map<CreateRecordStatusCommand>(createRecordStatusDto);
        var recordStatusId = await Mediator.Send(command);
        if (recordStatusId.IsFailed)
        {
            return ProblemResponse(recordStatusId.Error);
        }
        return Ok(recordStatusId);

    }
    /// <summary>
    /// Обновить информацию о статусе записи.
    /// </summary>
    /// <param name="updateRecordStatusDto">Данные для обновления статуса записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateRecordStatusDto updateRecordStatusDto)
    {

        var command = Mapper.Map<UpdateRecordStatusCommand>(updateRecordStatusDto);
        var recordStatusId = await Mediator.Send(command);
        if (recordStatusId.IsFailed)
        {
            return ProblemResponse(recordStatusId.Error);
        }
        return Ok(recordStatusId);
    }
    /// <summary>
    /// Удалить статус записи.
    /// </summary>
    /// <param name="id">Идентификатор статуса записи.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteRecordStatusCommand(id));
        if(command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }

        return NoContent();
    }
}
