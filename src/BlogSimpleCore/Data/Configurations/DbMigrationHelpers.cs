using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogSimpleCore.Data.Configurations
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await context.Database.MigrateAsync();

                await EnsureSeedPosts(context);
            }
        }

        private static async Task EnsureSeedPosts(ApplicationDbContext context)
        {
            if (context.Users.Any())
                return;

            #region Jose

            var UserId = Guid.NewGuid().ToString();

            await context.Users.AddAsync(new IdentityUser
            {
                Id = UserId,
                UserName = "jose.silva@teste.com",
                NormalizedUserName = "JOSE.SILVA@TESTE.COM",
                Email = "jose.silva@teste.com",
                NormalizedEmail = "JOSE.SILVA@TESTE.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAECMKSSgkIWp4tYMw8eMIApmS+1rt8iUew8pn0daWPtVk1YXuLWsbVO3MTGeDSWMGrA==", //Teste@123
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await context.SaveChangesAsync();

            var RoleId = "1";

            await context.Roles.AddAsync(new IdentityRole
            {
                Id = RoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = null
            });

            await context.SaveChangesAsync();

            await context.UserRoles.AddAsync(new IdentityUserRole<string>{
                UserId = UserId,
                RoleId = RoleId
            });

            await context.SaveChangesAsync();

            await context.Authors.AddAsync(new Models.Author
            {
                Id = UserId,
                Name = "Jose Silva"
            });

            await context.SaveChangesAsync();

            #endregion

            #region Maria
            var UserId1 = Guid.NewGuid().ToString();

            await context.Users.AddAsync(new IdentityUser
            {
                Id = UserId1,
                UserName = "maria.silva@teste.com",
                NormalizedUserName = "MARIA.SILVA@TESTE.COM",
                Email = "maria.silva@teste.com",
                NormalizedEmail = "MARIA.SILVA@TESTE.COM",                
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAECMKSSgkIWp4tYMw8eMIApmS+1rt8iUew8pn0daWPtVk1YXuLWsbVO3MTGeDSWMGrA==", //Teste@123
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await context.SaveChangesAsync();

            await context.Authors.AddAsync(new Models.Author
            {
                Id = UserId1,
                Name = "Maria Silva"
            });

            await context.SaveChangesAsync();

            #endregion

            #region Joao

            var UserId2 = Guid.NewGuid().ToString();

            await context.Users.AddAsync(new IdentityUser
            {
                Id = UserId2,
                UserName = "joao.silva@teste.com",
                NormalizedUserName = "JOAO.SILVA@TESTE.COM",
                Email = "joao.silva@teste.com",
                NormalizedEmail = "JOAO.SILVA@TESTE.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAECMKSSgkIWp4tYMw8eMIApmS+1rt8iUew8pn0daWPtVk1YXuLWsbVO3MTGeDSWMGrA==", //Teste@123
                TwoFactorEnabled = false,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            });

            await context.SaveChangesAsync();

            await context.Authors.AddAsync(new Models.Author
            {
                Id = UserId2,
                Name = "Joao Silva"
            });

            await context.SaveChangesAsync();

            #endregion

            #region Post

            if (context.Posts.Any())
                return;

            await context.Posts.AddAsync(new Models.Post
            {
                Title = "Title",
                Description = "Description",
                AuthorId = UserId,
                PublicationDate = DateTime.Now,
            });

            await context.SaveChangesAsync();

            await context.Posts.AddAsync(new Models.Post
            {
                Title = "Title 1",
                Description = "Description 1",
                AuthorId = UserId1,
                PublicationDate = DateTime.Now,
            });

            await context.SaveChangesAsync();

            await context.Posts.AddAsync(new Models.Post
            {
                Title = "Title 2",
                Description = "Description 2",
                AuthorId = UserId2,
                PublicationDate = DateTime.Now,
            });

            await context.SaveChangesAsync();

            #endregion
        }
    }
}
