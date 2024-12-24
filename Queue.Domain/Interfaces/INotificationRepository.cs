using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface INotificationRepository 
{
    Task<Result<List<Notification>>> GetAllAsync();
    Task<Result> AddAsync(Notification notification);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetNotificationdById(int id);
    Task<Result> UpdateAsync(int notificationId,int? notificationTypeId=null, string? nameRu=null,string? nameKk=null,string? nameEn=null,string? contentRu=null,string? contentKk=null,string? contentEn=null);
}
