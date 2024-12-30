using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using static System.Collections.Specialized.BitVector32;

namespace Queue.Persistence.Repository;

public class SqlRecordRepository(QueuesDbContext _dbContext, ILogger<SqlRecordRepository> logger) : IRecordRepository
{
    public async Task<Result> AddAsync(Record record)
    {
        try
        {
            logger.LogInformation("Добавление новой записи в базу данных.");
            await _dbContext.Records.AddAsync(record);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Запись {TargetFirstName},{TargetLastName} добавлена в базу данных.", record.FirstName,record.LastName);
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении записи {TargetFirstName},{TargetLastName} в базу данных.", record.FirstName, record.LastName);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении нового действия {record.FirstName},{record.LastName} в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление записи из базы данных.");
            var record = await _dbContext.Records.FindAsync(id);
            if (record != null)
            {
                _dbContext.Records.Remove(record);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Запись с id {TargeId} удалена из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Запись с id {TargetId} не найдена в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Запись с id {id} не найдена в базе данных."));

        }
        catch (Exception)
        {
            logger.LogError("Ошибка при удалении записи с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении записи с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Record>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка записей из базы данных.");
            var record = await _dbContext.Records.ToListAsync();
            logger.LogInformation("Полный список записей получен из базы данных.");
            return Result.Success(record);
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении полного списка записей из базы данных.");
            return Result.Failure<List<Record>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка записей из базы данных."));
        }
    }

    public async Task<Result> GetRecordById(int id)
    {
        try
        {
            logger.LogInformation("Получение записи из базы данных.");
            var record = await _dbContext.Records.FindAsync(id);
            if (record != null)
            {
                logger.LogInformation("Запись с id {TargetId} получено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Запись с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Запись с id {id} не найдено в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при получении записи с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении записи с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int recordId, string? firstName = null,string ? lastName = null, string? surName = null, string? iin = null, int? recordStatusId = null, int? serviceId = null, bool? isCreatedByEmployee = null, int? createdBy = null, int? ticketNumber = null)
    {
        try
        {
            logger.LogInformation("Обновление записи в базе данных.");
            var recordUpdate = await _dbContext.Records.FindAsync(recordId);
            if (recordUpdate != null)
            {
                recordUpdate.FirstName = firstName ?? recordUpdate.FirstName;
                recordUpdate.LastName = lastName ?? recordUpdate.LastName;
                recordUpdate.Surname = surName ?? recordUpdate.Surname;
                recordUpdate.Iin = iin ?? recordUpdate.Iin;
                recordUpdate.RecordStatusId = recordStatusId ?? recordUpdate.RecordStatusId;
                recordUpdate.ServiceId = serviceId ?? recordUpdate.ServiceId;
                recordUpdate.IsCreatedByEmployee = isCreatedByEmployee ?? recordUpdate.IsCreatedByEmployee;
                recordUpdate.CreatedBy = createdBy ?? recordUpdate.CreatedBy;
                recordUpdate.TicketNumber = ticketNumber ?? recordUpdate.TicketNumber;

                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Запись с id {TargetId} обновлено в базе данных.", recordId);
                return Result.Success();
            }

            logger.LogError("Запись с id {TargetId} не найдено в базе данных.", recordId);
            return Result.Failure(new Error(Errors.NotFound, $"Запись с id {recordId} не найдено в базе данных."));
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при обновлении записи с id {TargetId} в базе данных.", recordId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении записи с id {recordId} в базе данных."));
        }
    }
}
