using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserServices.Commands.UpdateUserService;

public record UpdateUserServiceCommand(
    int UserServiceId,
    int? UserId=null,
    int? ServiceId=null ,
    string? DescriptionRu=null,
    string? DescriptionKk=null,
    string? DescriptionEn=null,
    bool IsActive=true
    ) :IRequest<Result>;
