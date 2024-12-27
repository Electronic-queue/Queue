using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.UserServices.Commands.CreateUserService;

public  record CreateUserServiceCommand(
    int UserId=0,
    int ServiceId=0,
    string? DescriptionRu=null,
    string? DescriptionKk=null,
    string? DescriptionEn=null,
    int? CreatedBy=null
    ) :IRequest<Result>;
