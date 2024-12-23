using Microsoft.AspNetCore.Mvc;
using Queue.Application.Record.Commands.CreateRecord;
using Queue.Application.Record.Commands.UpdateRecord;
using Queue.Application.Record.Queries.GetRecodList;
using Queue.Application.Record.Queries.GetRecordById;
using Queue.Application.RecordStatus.Commands.CreateRecordStatus;
using Queue.Application.RecordStatus.Commands.DeleteRecordStatus;
using Queue.Application.RecordStatus.Commands.UpdateRecordStatus;
using Queue.Application.RecordStatus.Queries.GetRecordStatusById;
using Queue.Application.RecordStatus.Queries.GetRecordStatusList;
using Queue.Application.Windows.Queries.GetWindowDetails;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.RecordContracts;
using Queue.WebApi.Contracts.RecordStatusContracts;
using System.Net;

namespace Queue.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class RecordController : BaseController
{
    private readonly ILogger<WindowController> _logger;
    /// <summary>
    /// Получить список всех записей
    /// </summary>
    ///
    /// <returns>Возвращает список  записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Record>> GetAll()
    {
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();

        var query = new GetRecordListQuery();

        var vm = await Mediator.Send(query);

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
    public async Task<ActionResult<Record>> Get(int RecordId)
    {
        var query = new GetRecordByIdQuery(RecordId);


        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Создать новый  записи.
    /// </summary>
    /// <param name="createRecordDto">Данные новой записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateRecordDto createRecordDto)
    {
        var command = Mapper.Map<CreateRecordCommand>(createRecordDto);
        var recordId = await Mediator.Send(command);
        if (recordId.IsFailed)
        {
            return BadRequest(recordId);
        }
        return Ok(recordId);

    }
    /// <summary>
    /// Обновить информацию о  записи.
    /// </summary>
    /// <param name="updateRecordDto">Данные для обновления  записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateRecordDto updateRecordDto)
    {
        await Mediator.Send(new UpdateRecordCommand(updateRecordDto.RecordId,updateRecordDto.FirstName, updateRecordDto.LastName, updateRecordDto.Surname, updateRecordDto.Iin, updateRecordDto.RecordStatusId, updateRecordDto.ServiceId, updateRecordDto.IsCreatedByEmployee, updateRecordDto.CreatedBy, updateRecordDto.TicketNumber));
        return NoContent();
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
        var command = await Mediator.Send(new DeleteRecordStatusCommand(id));


        return NoContent();
    }
}


