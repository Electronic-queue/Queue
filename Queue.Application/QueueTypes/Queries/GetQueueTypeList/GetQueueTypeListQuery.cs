using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Entites;

namespace Queue.Application.QueueTypes.Queries.GetQueueTypeList;


public record GetQueueTypeListQuery() : IRequest <Result<List<QueueType>>>;
