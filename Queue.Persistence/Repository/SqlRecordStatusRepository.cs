using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository
{
    public class SqlRecordStatusRepository(QueuesDbContext _dbContext) : IRecordStatusRepository
    {
        public async  Task<Result> AddAsync(RecordStatus recordStatus)
        {
            try
            {
                await _dbContext.RecordStatuses.AddAsync(recordStatus);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
            }
        }

        public async Task<Result> DeleteAsync(int id)
        {
            try
            {
                var recordStatus = await _dbContext.RecordStatuses.FindAsync(id);
                if(recordStatus is null) 
                {
                    return Result.Failure(new Error(Errors.NotFound, "RecordStatus not Found"));
                }
                _dbContext.Remove(recordStatus);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
            }
        }

        public async Task<Result<List<RecordStatus>>> GetAllAsync()
        {
            try
            {
                var recordStatus = await _dbContext.RecordStatuses.ToListAsync();
                return Result.Success(recordStatus);
            }
            catch(Exception ex)
            {
                return Result.Failure<List<RecordStatus>>(new Error(Errors.InternalServerError, ex.Message));

            }
        }

        public async Task<Result> UpdateAsync(int recordStatusId, string? nameRu = null, string? nameKk = null, string? nameEn = null, string? descriptionRu = null, string? descriptionKk = null, string? descriptionEn = null, int? createdBy = null)
        {
            try
            {
                var recUpdate = await _dbContext.RecordStatuses.FindAsync(recordStatusId);
                if (recUpdate is null)
                {
                    return Result.Failure(new Error(Errors.NotFound, "RecordStatus not Found"));
                }
                recUpdate.NameRu = nameRu??recUpdate.NameRu;
                recUpdate.NameKk = nameKk?? recUpdate.NameKk;
                recUpdate.NameEn = nameEn?? recUpdate.NameEn;
                recUpdate.DescriptionRu = descriptionRu?? recUpdate.DescriptionRu;
                recUpdate.DescriptionKk = descriptionKk?? recUpdate.NameKk;
                recUpdate.DescriptionEn = descriptionEn?? recUpdate.DescriptionEn;
                recUpdate.CreatedBy = createdBy?? recUpdate.CreatedBy;
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
            }
        }

        public async Task<Result> GetRecordStatusById(int id)
        {
            try
            {
                var recordStatus = await _dbContext.RecordStatuses.FindAsync(id);
                if(recordStatus is null)
                {
                    return Result.Failure(new Error(Errors.NotFound, "RecordStatus not Found"));
                }
                return Result.Success(recordStatus);
            }
            catch(Exception ex)
            {
                return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
            }
        }
    }
}
