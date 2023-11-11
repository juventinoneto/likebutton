using Application.Interfaces;
using Domain.Interfaces;
using Domain.Services;
using Infrastructure.Queu;
using Infrastructure.Redis;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<RedisService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<SubscriberService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddHostedService<UpdateCacheBackgroundService>();
builder.Services.AddHostedService<SubscriberService>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" }); });

var assembly = AppDomain.CurrentDomain.Load("Application");
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

builder.Services.AddDbContext<LikeButtonContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

// Configure
var app = builder.Build();
if (app.Environment.IsDevelopment())
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

app.Run();
