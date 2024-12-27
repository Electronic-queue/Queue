using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.Reviews.Queries.GetReviewList;

public class GetReviewListQuery():IRequest<Result<List<Review>>>;
