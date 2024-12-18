using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;
using System.Collections.Generic;

namespace Queue.Domain.Interfaces;

public interface IRepository<T>
{
    Task<Result<List<T>>> GetAllAsync();
    Task<Result> AddAsync(T data);
    Task<Result> UpdateAsync(T data);
    Task<Result> DeleteAsync(Guid id);
    
}
