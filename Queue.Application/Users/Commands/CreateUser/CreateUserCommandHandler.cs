using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository _userRepository) : IRequestHandler<CreateUserCommand, Result>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Surname = request.Surname,
            Login = request.Login,
            PasswordHash = request.PasswordHash,
            CreatedOn = DateTime.FromFileTimeUtc(5),
            CreatedBy = request.CreatedBy,
            IsDeleted = request.IsDeleted,
        };
        var result = await _userRepository.AddAsync(user);
        if (result.IsFailed)
        {
            return Result.Failure<int>(new Error(Errors.BadRequest, "Ошибка такая-то"));
        }

        return Result.Success();
    }

}