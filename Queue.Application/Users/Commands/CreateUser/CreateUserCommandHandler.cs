using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository _userRepository) : IRequestHandler<CreateUserCommand, Result>
{
    private static readonly TimeSpan UtcOffset = TimeSpan.FromHours(5);
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Surname = request.Surname,
            Login = request.Login,
            PasswordHash = request.PasswordHash,
            CreatedOn = DateTimeOffset.UtcNow.ToOffset(UtcOffset).DateTime,
            CreatedBy = request.CreatedBy,
            IsDeleted = false,
        };
        var result = await _userRepository.AddAsync(user);
        if (result.IsFailed)
        {
            return Result.Failure<int>(new Error(Errors.BadRequest, "Неизвестная ошибка."));
        }
        if (request.FirstName == "" || request.LastName == "" || request.Login == "" || request.PasswordHash == "")
        {
            return Result.Failure<int>(new Error(Errors.BadRequest, "Обязательные поля должны быть заполнены."));
        }
        return Result.Success();
    }

}