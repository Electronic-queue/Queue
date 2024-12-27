using Microsoft.AspNetCore.Mvc;
using Queue.Application.UserWindows.Commands.CreateUserWindow;
using Queue.Application.UserWindows.Commands.DeleteUserWindow;
using Queue.Application.UserWindows.Commands.UpdateUserWindow;
using Queue.Application.UserWindows.Queries.GetUserWindowById;
using Queue.Application.UserWindows.Queries.GetUserWindowList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.UserWindowComtracts;
using System.Net;

namespace Queue.WebApi.Controllers
{

    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{apiversion:}/[controller]")]

    public class UserWindowController : BaseController
    {
        private readonly ILogger<UserWindowController> _logger;
        /// <summary>
        /// Получить список всех услуг.
        /// </summary>
        /// <returns>Возвращает список услуг.</returns>
        /// 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<UserWindow>>> GetAll()
        {
            var query = new GetUserWindowListQuery();
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
        /// <param name="userWindowId">Идентификатор услуги.</param>
        /// <returns>Возвращает детали услуги.</returns>
        [HttpGet("{userServiceId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserWindow>> Get(int userWindowId)
        {
            var query = new GetUserWindowByIdQuery(userWindowId);
            var vm = await Mediator.Send(query);
            if (vm.IsFailed)
            {
                return ProblemResponse(vm.Error);
            }
            return Ok(vm);
        }

        /// <summary>
        /// Создать новую услугу.
        /// </summary>
        /// <param name="createUserWindowDto">Данные новоой услуги.</param>
        /// <returns>Возвращает идентификатор созданной услуги.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<int>> Create([FromBody] CreateUserWindowDto createUserWindowDto)
        {
            var command = Mapper.Map<CreateUserWindowCommand>(createUserWindowDto);
            var userWindowId = await Mediator.Send(command);
            if (userWindowId.IsFailed)
            {
                return ProblemResponse(userWindowId.Error);
            }
            return Ok(userWindowId);

        }
        /// <summary>
        /// Обновить информацию об услуге.
        /// </summary>
        /// <param name="updateUserWindowDto">Данные для обновления услуги.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Update([FromBody] UpdateUserWindowDto updateUserWindowDto)
        {
            var command = Mapper.Map<UpdateUserWindowCommand>(updateUserWindowDto);
            var userWindowId = await Mediator.Send(command);
            if (userWindowId.IsFailed)
            {
                return ProblemResponse(userWindowId.Error);
            }
            return Ok(userWindowId);
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
            var command = await Mediator.Send(new DeleteUserWindowCommand(id));
            if(command.IsFailed)
            {
                return ProblemResponse(command.Error);
            }
            return NoContent();
        }
    }

}
