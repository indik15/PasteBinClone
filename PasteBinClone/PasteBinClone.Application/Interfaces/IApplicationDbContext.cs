using Microsoft.EntityFrameworkCore;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<ContentType> ContentTypes { get; }
        DbSet<Language> Languages { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
