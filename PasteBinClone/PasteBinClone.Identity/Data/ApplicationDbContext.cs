using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PasteBinClone.Identity.Models;

namespace PasteBinClone.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {  }

        public DbSet<AppUser> AppUsers {  get; set; }
    }
}
