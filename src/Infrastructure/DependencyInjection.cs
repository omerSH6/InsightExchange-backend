﻿using Domain.Interfaces.Authentication;
using Domain.Interfaces.Repositories;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<InsightExchangeDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IAnswerVoteRepository, AnswerVoteRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionVoteRepository, QuestionVoteRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
