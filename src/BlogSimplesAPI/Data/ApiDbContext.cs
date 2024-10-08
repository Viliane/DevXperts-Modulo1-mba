using BlogSimplesAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogSimplesAPI.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        }
    }
}
