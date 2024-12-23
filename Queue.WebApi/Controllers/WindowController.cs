using Microsoft.AspNetCore.Mvc;
using Queue.Application.Windows.Commands.CreateWindow;
using Queue.Application.Windows.Commands.DeleteWindow;
using Queue.Application.Windows.Commands.UpdateWindow;
using Queue.Application.Windows.Queries.GetWindowDetails;
using Queue.Application.Windows.Queries.GetWindowList;
using Queue.WebApi.Contracts.WIndowContracts;
using System.Net;

namespace Queue.WebApi.Controllers;


[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class WindowController : BaseController
{
    private readonly ILogger<WindowController> _logger;
    /// <summary>
    /// Получить список всех пользователей.
    /// </summary>
    /// <param name="id">Идентификатор пользователя (опционально).</param>
    /// <returns>Возвращает список пользователей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<WindowLIistVm>> GetAll(int WindowId)
    {
        var correlationId = HttpContext.Items["CorrelationId"]?.ToString();
        _logger.LogInformation("CorrelationId: {CorrelationId} - Получение списка пользователей.", correlationId);
        var query = new GetWindowListQuery
        {
            WindowId=WindowId
        };
        var vm = await Mediator.Send(query);
        _logger.LogInformation("CorrelationId: {CorrelationId} - Данные о пользователе с ID {UserId} получены.", correlationId, WindowId);
        return Ok(vm);
        //return ResultSucces.Success(vm);
    }

    /// <summary>
    /// Получить информацию о конкретном пользователе.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <returns>Возвращает детали пользователя.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<WindowDetailsVm>> Get(int WindowId)
    {
        var query = new GetWindowDetailsQuery
        {

            WindowId = WindowId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Создать нового пользователя.
    /// </summary>
    /// <param name="createWindowDto">Данные нового пользователя.</param>
    /// <returns>Возвращает идентификатор созданного пользователя.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateWindowDto createWindowDto)
    {
        var command = Mapper.Map<CreateWindowCommand>(createWindowDto);
        var windowId = await Mediator.Send(command);
        if (windowId.IsFailed)
        {
            return BadRequest(windowId);
        }
        return Ok(windowId);

    }
    /// <summary>
    /// Обновить информацию о пользователе.
    /// </summary>
    /// <param name="updateWindowDto">Данные для обновления пользователя.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateWindowDto updateWindowDto)
    {
        await Mediator.Send(new UpdateWindowCommand(updateWindowDto.WindowId, updateWindowDto.WindowNumber,updateWindowDto.WindowStatusId, updateWindowDto.CreatedBy));
        return NoContent();
    }
    /// <summary>
    /// Удалить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var command = await Mediator.Send(new DeleteWindowCommand(id));


        return NoContent();
    }
}
