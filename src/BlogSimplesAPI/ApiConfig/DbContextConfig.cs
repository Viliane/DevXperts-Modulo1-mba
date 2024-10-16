﻿using BlogSimplesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace BlogSimplesAPI.ApiConfig
{
    public static class DbContextConfig
    {
        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            return builder;
        }
    }
}
