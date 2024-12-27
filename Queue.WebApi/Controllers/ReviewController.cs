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

public class ReviewController : BaseController
{
    private readonly ILogger<ReviewController> _logger;
    /// <summary>
    /// Получить список всех записей
    /// </summary>
    ///
    /// <returns>Возвращает список  записей.</returns>
    /// 

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<Review>> GetAll()
    {


        var query = new GetReviewListQuery();

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
    public async Task<ActionResult<Review>> Get(int ReviewId)
    {
        var query = new GetReviewByIdQuery(ReviewId);
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
    /// <param name="createReviewDto">Данные новой записи.</param>
    /// <returns>Возвращает идентификатор созданного статуса записи.</returns>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<int>> Create([FromBody] CreateReviewDto createReviewDto)
    {
        var command = Mapper.Map<CreateReviewCommand>(createReviewDto);
        var reviewId = await Mediator.Send(command);
        if (reviewId.IsFailed)
        {
            return ProblemResponse(reviewId.Error);
        }
        return Ok(reviewId);

    }
    /// <summary>
    /// Обновить информацию о  записи.
    /// </summary>
    /// <param name="updateReviewDto">Данные для обновления  записи.</param>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Update([FromBody] UpdateReviewDto updateReviewDto)
    {
        var command = Mapper.Map<UpdateReviewCommand>(updateReviewDto);
        var reviewId = await Mediator.Send(command);
        if (reviewId.IsFailed)
        {
            return ProblemResponse(reviewId.Error);
        }
        return Ok(reviewId);
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
        var command = await Mediator.Send(new DeleteReviewCommand(id));

        if(command.IsFailed)
        {
            return ProblemResponse(command.Error);
        }
        return NoContent();
    }
}

