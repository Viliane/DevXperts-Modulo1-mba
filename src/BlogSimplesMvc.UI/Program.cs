using BlogSimplesMvc.UI.Data.Configurations;
using BlogSimplesMvc.UI.MvcConfig;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbContextConfig()
    .AddIdentityConfig()
    .AddDependencyInjectionConfig();

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
