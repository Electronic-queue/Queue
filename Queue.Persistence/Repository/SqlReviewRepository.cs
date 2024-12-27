using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Common.Exceptions;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence.Repository;

public class SqlReviewRepository (QueuesDbContext _dbContext): IReviewRepository
{
    public async Task<Result> AddAsync(Review review)
    {
        try
        {
            await _dbContext.Reviews.AddAsync(review);
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
            var review = await _dbContext.Reviews.FindAsync(id);
            if(review is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Review not Found"));
            }
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<Review>>> GetAllAsync()
    {
        try
        {
            var review=await _dbContext.Reviews.ToListAsync();
            return Result.Success(review);

                     
        }
        catch(Exception ex)
        {
            return Result.Failure<List<Review>>(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> GetReviewdById(int id)
    {
        try
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if( review is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Review not Found"));
            }
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }

    public async Task<Result> UpdateAsync(int reviewId, int? recordId = null, int? rating = null, string? content = null)
    {
        try
        {
            var review = await _dbContext.Reviews.FindAsync(reviewId);
            if(review is null)
            {
                return Result.Failure(new Error(Errors.NotFound, "Review not Found"));
            }
            review.RecordId = recordId ?? review.RecordId;
            review.Rating = rating ?? review.Rating;
            review.Content = content ?? review.Content;
            await _dbContext.SaveChangesAsync();
            return Result.Success();
        }
        catch(Exception ex)
        {
            return Result.Failure(new Error(Errors.InternalServerError, ex.Message));
        }
    }
}
