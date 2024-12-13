using Microsoft.EntityFrameworkCore;
using Queue.Application.Interfaces;
using Queue.Domain;
using Queue.Persistence.EntityTypeConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Persistence
{
    public class QueuesDbContext:DbContext,IQueuesDbContext
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
