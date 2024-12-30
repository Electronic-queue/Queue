using KDS.Primitives.FluentResult;
using Microsoft.AspNetCore.Mvc;
using Queue.Application.Reviews.Commands.CreateReview;
using Queue.Application.Reviews.Commands.DeleteReview;
using Queue.Application.Reviews.Commands.UpdateReview;
using Queue.Application.Reviews.Queries.GetReviewById;
using Queue.Application.Reviews.Queries.GetReviewList;
using Queue.Domain.Entites;
using Queue.WebApi.Contracts.ReviewContracts;
using System.Net;

namespace Queue.WebApi.Controllers;

[ApiVersion("1.0")]
[Produces("application/json")]
[Route("api/{apiversion:}/[controller]")]

public class ReviewController(ILogger<ReviewController> _logger) : BaseController
{

    /// <summary>
    /// Получить список всех записей
    /// </summary>
    ///
    /// <returns>Возвращает список  записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Review>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> GetAll()
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение полного списка отзывов.");
            var query = new GetReviewListQuery();

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
    /// Получить информацию о конкретной записи.
    /// </summary>
    /// <param name="reviewId">Идентификатор  записи.</param>
    /// <returns>Возвращает детали  записи.</returns>
    [HttpGet("{reviewId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Review))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Get(int reviewId)
    {
        var scope = new Dictionary<string, object>() { { "ReviewId", reviewId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на чтение отзыва с id {Id}", reviewId);
            var query = new GetReviewByIdQuery(reviewId);
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
    /// Создать новый  записи.
    /// </summary>
    /// <param name="createReviewDto">Данные новой записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Create([FromBody] CreateReviewDto createReviewDto)
    {
        var scope = new Dictionary<string, object>();
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на создание отзыва");
            var result = await Mediator.Send(Mapper.Map<CreateReviewCommand>(createReviewDto));
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
    /// Обновить информацию о  записи.
    /// </summary>
    /// <param name="updateReviewDto">Данные для обновления  записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]

    public async Task<IActionResult> Update([FromBody] UpdateReviewDto updateReviewDto)
    {
        var scope = new Dictionary<string, object>() { { "ReviewId", updateReviewDto.ReviewId } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на обновление отзыва с id {Id}", updateReviewDto.ReviewId);
            var result = await Mediator.Send(Mapper.Map<UpdateReviewCommand>(updateReviewDto));
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
    /// Удалить  записи.
    /// </summary>
    /// <param name="id">Идентификатор  записи.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Result))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Result))]
    public async Task<IActionResult> Delete(int id)
    {
        var scope = new Dictionary<string, object>() { { "ReviewId", id } };
        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Отправка запроса на удаление отзыва с id {Id}", id);
            var result = await Mediator.Send(new DeleteReviewCommand(id));

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

