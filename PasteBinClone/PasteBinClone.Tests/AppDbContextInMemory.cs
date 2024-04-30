using Microsoft.EntityFrameworkCore;
using PasteBinClone.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests
{
    public static class AppDbContextInMemory
    {
        public static ApplicationDbContext GetContextInMemory()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var context = new ApplicationDbContext(dbContextOptions.Options);

            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
