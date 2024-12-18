using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;
using Queue.Domain.Entites;
using Queue.Domain.Interfaces;

namespace Queue.Persistence
{
    public class SqlUserRepository(QueuesDbContext _dbContext) : IUserRepository
    {
        public async Task<Result> AddAsync(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("DatabaseError", ex.Message));
            }
            
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var user=await _dbContext.Users.FindAsync(id);
            if(user != null)
            {
                return Result.Failure(new Error("Not Found", "User Not Found"));
            }
            try
            {
                user.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("DatabaseError",ex.Message));
            }
        }

        

        public async  Task<Result<List<User>>> GetAllAsync()
        {
            try
            {
                var user= await _dbContext.Users.ToListAsync();
                return Result.Success(user);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<User>>(new Error("DatabaseError", ex.Message));


            }
        }

        public async  Task<Result> GetUserById(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if(user != null)
                {
                    return Result.Failure(new Error("NotFound", "UserNotFound"));

                }
                return Result.Success(user);
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("DatabaseError", ex.Message));

            }
        }

        public async Task<Result> GetUserDetails(Guid id)
        {
            try
            {
                var userResult = await _dbContext.Users.FindAsync(id);
                if(userResult != null)
                {
                    return Result.Failure(new Error("101", "User not Found"));

                }
                return Result.Success(userResult);
            }
            catch(Exception ex)
            {
                return Result.Failure(new Error("404", "DatabaseError"));
            }
        }

        public async Task<Result> UpdateAsync(User user)
        {
            try
            {
                var updateUser = await _dbContext.Users.FindAsync(user.Id);
                if (updateUser != null)
                {
                    return Result.Failure(new Error("NotFound", "UserNotFound"));

                }
                updateUser.Iin = user.Iin;
                updateUser.FirstName = user.FirstName;
                updateUser.LastName = user.LastName;
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            }

            catch (Exception ex)
            {
                return Result.Failure(new Error("DatabaseError", ex.Message));
            }

        }

        

        
    }
}
