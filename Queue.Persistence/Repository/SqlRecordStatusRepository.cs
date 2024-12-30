using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using System.Data;

namespace Queue.Persistence.Repository
{
    public class SqlRecordStatusRepository(QueuesDbContext _dbContext, ILogger<SqlRecordStatusRepository> logger) : IRecordStatusRepository
    {
        public async Task<Result> AddAsync(RecordStatus recordStatus)
        {

            try
            {
                logger.LogInformation("Добавление нового статуса записи в базу данных.");
                await _dbContext.RecordStatuses.AddAsync(recordStatus);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Статус записи {TargetNameRu}({TargetNameKk}, {TargetNameEn}) добавлена в базу данных.", recordStatus.NameRu, recordStatus.NameKk, recordStatus.NameEn);
                return Result.Success();
            }
            catch (Exception)
            {
                logger.LogError("Ошибка при добавлении статуса записи {TargetNameRu}({TargetNameKk}, {TargetNameEn}) в базу данных.", recordStatus.NameRu, recordStatus.NameKk, recordStatus.NameEn);
                return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении статуса записи {recordStatus.NameRu}({recordStatus.NameKk}, {recordStatus.NameEn}) в базу данных."));
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                logger.LogInformation("Удаление статуса записи из базы данных.");
                var recordStatus = await _dbContext.RecordStatuses.FindAsync(id);
                if (recordStatus != null)
                {
                    _dbContext.Remove(recordStatus);
                    await _dbContext.SaveChangesAsync();
                    logger.LogInformation("Статус записи с id {TargetId} удалена из базы данных.", id);
                    return Result.Success();
                }
                logger.LogError("Статус записи с id {TargetId} не найдена в базе данных.", id);
                return Result.Failure(new Error(Errors.NotFound, $"Статус записи с id {id} не найдена в базе данных."));

            }
            catch (Exception)
            {
                logger.LogError("Ошибка при удалении статуса записи с id {TargetId} из базы данных.", id);
                return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении статуса записи с id {id} из базы данных."));
            }
        }

        public async Task<Result<List<RecordStatus>>> GetAllAsync()
        {
            try
            {
                logger.LogInformation("Получение полного списка статуса записей из базы данных.");
                var recordStatus = await _dbContext.RecordStatuses.ToListAsync();
                logger.LogInformation("Полный список статуса записей получен из базы данных.");
                return Result.Success(recordStatus);
            }
            catch (Exception)
            {
                logger.LogError("Ошибка при получении полного списка статуса записей из базы данных.");
                return Result.Failure<List<RecordStatus>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка статуса записей из базы данных."));

            }
        }

        public async Task<Result> UpdateAsync(int recordStatusId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null, int? createdBy = null)
        {
            try
            {
                logger.LogInformation("Обновление статуса записи в базе данных.");
                var recUpdate = await _dbContext.RecordStatuses.FindAsync(recordStatusId);
                if (recUpdate != null)
                {
                    recUpdate.NameRu = nameRu ?? recUpdate.NameRu;
                    recUpdate.NameKk = nameKk ?? recUpdate.NameKk;
                    recUpdate.NameEn = nameEn ?? recUpdate.NameEn;
                    recUpdate.DescriptionRu = descriptionRu ?? recUpdate.DescriptionRu;
                    recUpdate.DescriptionKk = descriptionKk ?? recUpdate.NameKk;
                    recUpdate.DescriptionEn = descriptionEn ?? recUpdate.DescriptionEn;
                    recUpdate.CreatedBy = createdBy ?? recUpdate.CreatedBy;
                    await _dbContext.SaveChangesAsync();
                    logger.LogInformation("Статус записи с id {TargetId} обновлена в базе данных.", recordStatusId);
                    return Result.Success();
                }
                logger.LogError("Статус записи с id {TargetId} не найдена в базе данных.", recordStatusId);
                return Result.Failure<Role>(new Error(Errors.NotFound, $"Статус записи с id {recordStatusId} не найдена в базе данных."));
            }
            catch (Exception)
            {
                logger.LogError("Ошибка при обновлении статуса записи с id {TargetId} в базе данных.", recordStatusId);
                return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении статуса записи с id {recordStatusId} в базе данных."));
            }
        }

        public async Task<Result> GetRecordStatusById(int id)
        {
            try
            {
                logger.LogInformation("Получение статуса записи из базы данных.");
                var recordStatus = await _dbContext.RecordStatuses.FindAsync(id);
                if (recordStatus != null)
                {
                    logger.LogInformation("Статус записи с id {TargetId} получена из базы данных.", id);
                    return Result.Success(recordStatus);
                }
                logger.LogError("Статус записи с id {TargetId} не найдена в базе данных.", id);
                return Result.Failure(new Error(Errors.NotFound, $"Статус записи с id {id} не найдена в базе данных."));

            }
            catch (Exception)
            {
                logger.LogError("Ошибка при получении статуса записи с id {TargetId} из базы данных.", id);
                return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении статуса записи с id {id} из базы данных."));
            }
        }
    }
}
