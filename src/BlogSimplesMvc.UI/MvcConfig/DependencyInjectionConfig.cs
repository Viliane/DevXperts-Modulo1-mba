using BlogSimplesMvc.UI.Data;
using BlogSimplesMvc.UI.Data.Repositories;
using BlogSimplesMvc.UI.Data.Repositories.Interfaces;
using BlogSimplesMvc.UI.Models;
using BlogSimplesMvc.UI.Services;
using BlogSimplesMvc.UI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogSimplesMvc.UI.MvcConfig
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
