using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface IUserServiceRepository
{
    Task<Result<List<UserService>>> GetAllAsync();
    Task<Result> AddAsync(UserService userService);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetUserServiceById(int id);
    Task<Result> UpdateAsync(int userServiceId,int? userId=null,int? serviceId=null,string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null,bool? isActive=null);

}
