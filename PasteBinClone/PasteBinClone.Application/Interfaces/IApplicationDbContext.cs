using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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
        DbSet<ApiUser> ApiUsers { get; }
        DbSet<Paste> Pastes { get; }
        DbSet<Comment> Comments { get; }
        DbSet<Rating> Ratings { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
        DatabaseFacade Database { get; }
    }
}
