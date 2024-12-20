using KDS.Primitives.FluentResult;
using MediatR;

namespace Queue.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    
    string FirstName, 
    string LastName, 
    string? Surname, 
    string Login, 
    string PasswordHash, 
    int? CreatedBy
    ) : IRequest<Result>;
