using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Data;

namespace Queue.Persistence.Repository;

public class SqlServiceRepository(QueuesDbContext _dbContext, ILogger<SqlServiceRepository> logger) : IServiceRepository
{
    public async Task<Result> AddAsync(Service service)
    {
        try
        {
            logger.LogInformation("Добавление новой услуги в базу данных.");
            await _dbContext.AddAsync(service);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Услуга {TargetNameRu}({TargetNameKk}, {TargetNameEn}) добавлена в базу данных.", service.NameRu, service.NameKk, service.NameEn);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении услуги {TargetNameRu}({TargetNameKk}, {TargetNameEn}) в базу данных.", service.NameRu, service.NameKk, service.NameEn);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении роли {service.NameRu}({service.NameKk}, {service.NameEn}) в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление услуги из базы данных.");
            var service = await _dbContext.Services.FindAsync(id);
            if (service is null)
            {
                _dbContext.Services.Remove(service);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Услуга с id {TargetId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Услуга с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Услуга с id {id} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при удалении услуги с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении услуги с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Service>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка услуг из базы данных.");
            var Service = await _dbContext.Services.ToListAsync();
            logger.LogInformation("Полный список услуг получен из базы данных.");
            return Result.Success(Service);
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении полного списка услуг из базы данных.");
            return Result.Failure<List<Service>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка услуг из базы данных."));
        }
    }

    public async Task<Result> GetServiceById(int id)
    {
        try
        {
            logger.LogInformation("Получение услуги из базы данных.");
            var service = await _dbContext.Services.FindAsync(id);
            if (service != null)
            {
                logger.LogInformation("Услуга с id {TargetId} получена из базы данных.", id);
                return Result.Success(service);
            }
            logger.LogError("Услуга с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Услуга с id {id} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении услуги с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении услуги с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int serviceId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null, int? avarageExecutionTime = null, int? parentServiceId = null, int? queueTypeId = null)
    {
        try
        {
            logger.LogInformation("Обновление услуги в базе данных.");
            var serviceUpdate = await _dbContext.Services.FindAsync(serviceId);
            if (serviceUpdate != null)
            {
                serviceUpdate.NameRu = nameRu ?? serviceUpdate.NameRu;
                serviceUpdate.NameKk = nameKk ?? serviceUpdate.NameKk;
                serviceUpdate.NameEn = nameEn ?? serviceUpdate.NameEn;
                serviceUpdate.DescriptionRu = descriptionRu ?? serviceUpdate.DescriptionRu;
                serviceUpdate.DescriptionKk = descriptionKk ?? serviceUpdate.DescriptionKk;
                serviceUpdate.DescriptionEn = descriptionEn ?? serviceUpdate.DescriptionEn;
                serviceUpdate.AverageExecutionTime = avarageExecutionTime ?? serviceUpdate.AverageExecutionTime;
                serviceUpdate.ParentserviceId = parentServiceId ?? serviceUpdate.ParentserviceId;
                serviceUpdate.QueueTypeId = queueTypeId ?? serviceUpdate.QueueTypeId;
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Услуга с id {TargetId} обновлена в базе данных.", serviceId);
                return Result.Success();
            }
            logger.LogError("Услуга с id {TargetId} не найдена в базе данных.", serviceId);
            return Result.Failure<Role>(new Error(Errors.NotFound, $"Услуга с id {serviceId} не найдена в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении услуги с id {TargetId} в базе данных.", serviceId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении услуги с id {serviceId} в базе данных."));
        }
    }
}
