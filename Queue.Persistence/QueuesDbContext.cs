using Microsoft.EntityFrameworkCore;
using Queue.Application.Interfaces;
using Queue.Domain.Entites;
using Queue.Persistence.EntityTypeConfigurations;

namespace Queue.Persistence
{
    public class QueuesDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public QueuesDbContext(DbContextOptions <QueuesDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration()); 
            base.OnModelCreating(builder); 
        }

    }
}
