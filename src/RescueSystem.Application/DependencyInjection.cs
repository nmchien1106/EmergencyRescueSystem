using System.Reflection;
using System.Reflection.Metadata;
using Autofac.Core;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RescueSystem.Application.Common.Behaviors;


namespace RescueSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // Add mediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            // Register FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            // Register Pipeline Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Register automapper
            services.AddAutoMapper(_ => { }, assembly);
            return services;
        }
    }
}
