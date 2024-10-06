using BlogSimplesMvc.UI.Data;
using BlogSimplesMvc.UI.Data.Configurations;
using BlogSimplesMvc.UI.Data.Repositories;
using BlogSimplesMvc.UI.Data.Repositories.Interfaces;
using BlogSimplesMvc.UI.Models;
using BlogSimplesMvc.UI.Services;
using BlogSimplesMvc.UI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuthorServices, AuthorServices>();
builder.Services.AddScoped<IPostServices, PostServices>();
builder.Services.AddScoped<ICommentServices, CommentsServices>();
builder.Services.AddScoped(typeof(IRepository<Author>), typeof(RepositoryAuthor));
builder.Services.AddScoped(typeof(IRepository<Post>), typeof(RepositoryPost));
builder.Services.AddScoped(typeof(IRepository<Comments>), typeof(RepositoryComment));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.UseDbMigrationHelper();

app.Run();
