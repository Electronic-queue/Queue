using KDS.Primitives.FluentResult;
using MediatR;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository _userRepository) : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {


        
        var user = new User
        {
            Id = Guid.NewGuid(),
            Iin = request.Iin,
            FirstName = request.FirstName,
            LastName = request.LastName,
           
            CreationDate = DateTime.FromFileTimeUtc(5)
        };
        var result = await _userRepository.AddAsync(user);
        if (result.IsFailed)
        {
            return Result.Failure<Guid>(new Error(Errors.BadRequest, "Ошибка такая-то"));
        }




        return Result.Success(user.Id);
    }
}


