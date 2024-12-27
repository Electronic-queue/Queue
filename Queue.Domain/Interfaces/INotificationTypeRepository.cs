using KDS.Primitives.FluentResult;
using Queue.Domain.Entites;

namespace Queue.Domain.Interfaces;

public interface INotificationTypeRepository 
{
    Task<Result<List<NotificationType>>> GetAllAsync();
    Task<Result> AddAsync(NotificationType notificationType);
    Task<Result> DeleteAsync(int id);
    Task<Result> GetNotificationTypeById(int id);
    Task<Result> UpdateAsync(int notificationTypeId,string? nameRu=null,string? nameKk=null,string? nameEn=null,string? descriptionRu=null,string? descriptionKk=null,string? descriptionEn=null);
}
