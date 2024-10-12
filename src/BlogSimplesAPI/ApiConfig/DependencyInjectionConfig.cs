using BlogSimplesAPI.Data.Repositories;
using BlogSimplesAPI.Data.Repositories.Interfaces;
using BlogSimplesAPI.Models;
using BlogSimplesAPI.Services;
using BlogSimplesAPI.Services.Interfaces;
using Microsoft.OpenApi.Models;

namespace BlogSimplesAPI.ApiConfig
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthorServices, AuthorServices>();
            builder.Services.AddScoped<IPostServices, PostServices>();
            builder.Services.AddScoped<ICommentServices, CommentsServices>();
            builder.Services.AddScoped(typeof(IRepository<Author>), typeof(RepositoryAuthor));
            builder.Services.AddScoped(typeof(IRepository<Post>), typeof(RepositoryPost));
            builder.Services.AddScoped(typeof(IRepository<Comments>), typeof(RepositoryComment));

            return builder;
        }
    }
}
