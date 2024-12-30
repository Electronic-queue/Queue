using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.NotificationTypes.Queries.GetNotificationTypeDetails;

public record GetNotificationTypeDetailsQuery(int NotificationTypeId) : IRequest<Result>;
