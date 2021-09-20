using System;
using System.Linq;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure;
using Infrastructure.Queu;
using Infrastructure.Redis;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ArticleService>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<RedisService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<SubscriberService>();
            services.AddScoped<IArticleRepository, ArticleRepository>();

            services.AddHostedService<UpdateCacheBackgroundService>();
            services.AddHostedService<SubscriberService>();

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"}); });
            services.AddMediatR(typeof(Startup));

            var assembly = AppDomain.CurrentDomain.Load("Application");
            services.AddMediatR(assembly);

            services.AddDbContext<LikeButtonContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
