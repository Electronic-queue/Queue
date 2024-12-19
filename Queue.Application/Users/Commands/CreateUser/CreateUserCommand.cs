using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string FirstName, 
    string LastName, 
    string? Surname, 
    string Login, 
    string PasswordHash, 
    DateTime CreatedOn, 
    int? CreatedBy,
    bool IsDeleted) : IRequest<Result>;
