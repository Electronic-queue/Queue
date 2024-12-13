using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(QueuesDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
