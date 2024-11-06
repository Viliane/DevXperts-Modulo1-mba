using BlogSimpleCore.Data.Repositories;
using BlogSimpleCore.Data.Repositories.Interfaces;
using BlogSimpleCore.Models;
using BlogSimpleCore.Services;
using BlogSimpleCore.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSimpleCore.Config
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
