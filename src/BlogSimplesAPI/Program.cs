using BlogSimplesAPI.Data;
using BlogSimplesAPI.Data.Repositories;
using BlogSimplesAPI.Data.Repositories.Interfaces;
using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IAuthorServices, AuthorServices>();
builder.Services.AddScoped<IPostServices, PostServices>();
builder.Services.AddScoped<ICommentServices, CommentsServices>();
builder.Services.AddScoped(typeof(IRepository<Author>), typeof(RepositoryAuthor));
builder.Services.AddScoped(typeof(IRepository<Post>), typeof(RepositoryPost));
builder.Services.AddScoped(typeof(IRepository<Comments>), typeof(RepositoryComment));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
