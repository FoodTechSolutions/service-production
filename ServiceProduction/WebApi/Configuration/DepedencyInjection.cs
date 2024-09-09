using Application.BackgroundServices;
using Application.Services.Interface;
using Application.Services;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Repositories;

namespace WebApi.Configuration
{
    public static class DepedencyInjection
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IProductIngredientRepository, ProductIngredientRepository>();
            services.AddScoped<IProductionProductRepository, ProductionProductRepository>();
            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductionService, ProductionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IProductIngredientRepository, ProductIngredientRepository>();
            services.AddScoped<IProductionRepository, ProductionRepository>();
            services.AddScoped<IProductionProductRepository, ProductionProductRepository>();

            services.AddScoped<IRabbitMqService, RabbitMqService>();
            services.AddScoped<IProcessEventExampleService, ProcessEventExampleService>();

            services.AddScoped<IProcessEventExampleService, ProcessEventExampleService>();


            services.AddScoped<IProcessEventExampleService, ProcessEventExampleService>();

            services.AddScoped<ICancelProductionService, CancelProductionService>();
            services.AddScoped<ICreateProductionService, CreateProductionService>();
            services.AddScoped<IStartProductionService, StartProductionService>();
            services.AddScoped<IFinishProductionService, FinishProductionService>();



            return services;
        }



        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<RabbitMqExampleHandler>();
            services.AddHostedService<CreateProductionHandler>();
            //services.AddHostedService<StartProductionHandler>();
            //services.AddHostedService<FinishProductionHandler>();

            return services;
        }
    }
}
