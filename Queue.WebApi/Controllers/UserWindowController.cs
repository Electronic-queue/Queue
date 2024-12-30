using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.UserWindows.Commands.CreateUserWindow;
using Queue.Application.UserWindows.Commands.DeleteUserWindow;
using Queue.Application.UserWindows.Commands.UpdateUserWindow;
using Queue.Application.UserWindows.Queries.GetUserWindowById;
using Queue.Application.UserWindows.Queries.GetUserWindowList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.UserServiceContracts;
using Queue.WebApi.Contracts.UserWindowComtracts;
using System.Net;

namespace Queue.WebApi.Controllers
{

    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{apiversion:}/[controller]")]

    public class UserWindowController(ILogger<UserWindowController> _logger) : BaseController
    {
        /// <summary>
        /// Получить список всех услуг.
        /// </summary>
        /// <returns>Возвращает список услуг.</returns>
        /// 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserWindow>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
        public async Task<IActionResult> GetAll()
        {
            var scope = new Dictionary<string, object>();
            using (_logger.BeginScope(scope))
            {
                _logger.LogInformation("Отправка запроса на чтение полного списка userService.");
                var query = new GetUserWindowListQuery();
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
        /// <param name="userWindowId">Идентификатор услуги.</param>
        /// <returns>Возвращает детали услуги.</returns>
        [HttpGet("{userWindowId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWindow))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
        public async Task<IActionResult> Get(int userWindowId)
        {
            var scope = new Dictionary<string, object>() { { "UserWindowId", userWindowId } };
            using (_logger.BeginScope(scope))
            {
                _logger.LogInformation("Отправка запроса на чтение userWindow с id {Id}", userWindowId);
                var query = new GetUserWindowByIdQuery(userWindowId);
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
        /// <param name="createUserWindowDto">Данные новоой услуги.</param>
        /// <returns>Возвращает идентификатор созданной услуги.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]


        public async Task<IActionResult> Create([FromBody] CreateUserWindowDto createUserWindowDto)
        {
            var scope = new Dictionary<string, object>();
            using (_logger.BeginScope(scope))

            {
                _logger.LogInformation("Отправка запроса на создание userWindow");
                var result = await Mediator.Send(Mapper.Map<CreateUserWindowCommand>(createUserWindowDto));
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
        /// <param name="updateUserWindowDto">Данные для обновления услуги.</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

        public async Task<IActionResult> Update([FromBody] UpdateUserWindowDto updateUserWindowDto)
        {
            var scope = new Dictionary<string, object>() { { "UserWIndowId", updateUserWindowDto.UserWindowId } };
            using (_logger.BeginScope(scope))
            {
                _logger.LogInformation("Отправка запроса на обновление userWindow с id {Id}", updateUserWindowDto.UserWindowId);
                var result = await Mediator.Send(Mapper.Map<UpdateUserWindowCommand>(updateUserWindowDto));
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
            var scope = new Dictionary<string, object>() { { "UserWindowId", id } };
            using (_logger.BeginScope(scope))
            {
                _logger.LogInformation("Отправка запроса на удаление userWindow с id {Id}", id);
                var result = await Mediator.Send(new DeleteUserWindowCommand(id));
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

}
