using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;
using System;
using static System.Collections.Specialized.BitVector32;

namespace Queue.Persistence.Repository;

public class SqlReviewRepository (QueuesDbContext _dbContext, ILogger<SqlReviewRepository> logger) : IReviewRepository
{
    public async Task<Result> AddAsync(Review review)
    {
        try
        {
            logger.LogInformation("Добавление нового отзыва в базу данных.");
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            logger.LogInformation("Отзыв добавлен в базу данных.");
            return Result.Success();
        }
        catch (Exception)
        {
            logger.LogError("Ошибка при добавлении отзыва в базу данных.");
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при добавлении нового отзыва в базу данных."));
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            logger.LogInformation("Удаление отзыва из базы данных.");
            var review = await _dbContext.Reviews.FindAsync(id);
            if(review != null)
            {
                 _dbContext.Reviews.Remove(review);
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Отзыв с id {TargetId} удалено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Отзыв с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Отзыв с id {id} не найдено в базе данных."));
        }
        catch(Exception)
        {
            logger.LogError("Ошибка при удалении отзыва с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при удалении отзыва с id {id} из базы данных."));
        }
    }

    public async Task<Result<List<Review>>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Получение полного списка отзывов из базы данных.");
            var review=await _dbContext.Reviews.ToListAsync();
            logger.LogInformation("Полный список отзывов получен из базы данных.");
            return Result.Success(review);
            
        }
        catch(Exception )
        {
            logger.LogError("Ошибка при получении полного списка отзывов из базы данных.");
            return Result.Failure<List<Review>>(new Error(Errors.InternalServerError, "Ошибка при получении полного списка отзывов из базы данных."));
        }
    }

    public async Task<Result> GetReviewdById(int id)
    {
        try
        {
            logger.LogInformation("Получение отзыва из базы данных.");
            var review = await _dbContext.Reviews.FindAsync(id);
            if( review != null)
            {
                logger.LogInformation("Отзыв с id {TargetId} получено из базы данных.", id);
                return Result.Success();
            }
            logger.LogError("Отзыв с id {TargetId} не найдено в базе данных.", id);
            return Result.Failure(new Error(Errors.NotFound, $"Отзыв с id {id} не найдено в базе данных."));

        }
        catch(Exception ex)
        {
            logger.LogError("Ошибка при получении отзыва с id {TargetId} из базы данных.", id);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при получении отзыва с id {id} из базы данных."));
        }
    }

    public async Task<Result> UpdateAsync(int reviewId, int? recordId = null, int? rating = null, string? content = null)
    {
        try
        {
            logger.LogInformation("Обновление отзыва в базе данных.");
            var review = await _dbContext.Reviews.FindAsync(reviewId);
            if(review != null)
            {
                review.RecordId = recordId ?? review.RecordId;
                review.Rating = rating ?? review.Rating;
                review.Content = content ?? review.Content;
                await _dbContext.SaveChangesAsync();
                await _dbContext.SaveChangesAsync();
                logger.LogInformation("Отзыв с id {TargetId} обновлено в базе данных.", reviewId);
                return Result.Success();
            }
            logger.LogError("Отзыв с id {TargetId} не найдено в базе данных.", reviewId);
            return Result.Failure(new Error(Errors.NotFound, $"Действие с id {reviewId} не найдено в базе данных."));
        }
        catch(Exception )
        {
            logger.LogError("Ошибка при обновлении отзыва с id {TargetId} в базе данных.", reviewId);
            return Result.Failure(new Error(Errors.InternalServerError, $"Ошибка при обновлении отзыва с id {reviewId} в базе данных."));
        }
    }
}
