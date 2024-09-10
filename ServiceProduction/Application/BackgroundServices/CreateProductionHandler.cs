﻿using Application.BackgroundServices.Models;
using Application.Helpers;
using Application.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.BackgroundServices
{
    public class CreateProductionHandler : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CreateProductionHandler> _logger;
        private string RABBIT_HOST;
        private string RABBIT_PORT;
        private string RABBIT_USERNAME;
        private string RABBIT_PASSWORD;

        public CreateProductionHandler(
        IServiceProvider serviceProvider,
        ILogger<CreateProductionHandler> logger,
        IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            RABBIT_HOST = configuration.GetSection("RabbitMqSettings")["HOST"] ?? string.Empty;
            RABBIT_PORT = configuration.GetSection("RabbitMqSettings")["PORT"] ?? string.Empty;
            RABBIT_USERNAME = configuration.GetSection("RabbitMqSettings")["USERNAME"] ?? string.Empty;
            RABBIT_PASSWORD = configuration.GetSection("RabbitMqSettings")["PASSWORD"] ?? string.Empty;
            CreateConnection();
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CreateConsumer();
        }

        private void CreateConsumer()
        {
            var consumer = new EventingBasicConsumer(_channel);
            //_channel.ModelShutdown += async (s, e) => await Channel_Shutdown(s, e);
            consumer.Received += async (s, e) => await ProcessEventAsync(s, e);
            _channel.BasicQos(0, 20, false);
            _channel.BasicConsume(EventConstants.CREATE_PRODUCTION_QUEUE, false, consumer);
        }

        private void CreateConnection()
        {

            try
            {
                _connection = new ConnectionFactory
                {
                    AmqpUriSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    Endpoint = new AmqpTcpEndpoint(RABBIT_HOST, int.Parse(RABBIT_PORT)),
                    UserName = RABBIT_USERNAME,
                    Password = RABBIT_PASSWORD
                }.CreateConnection();

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(
                    exchange: EventConstants.CREATE_PRODUCTION_EXCHANGE,
                    type: ExchangeType.Direct);

                _channel.QueueDeclare(
                    queue: EventConstants.CREATE_PRODUCTION_QUEUE);

                _channel.QueueBind(
                    exchange: EventConstants.CREATE_PRODUCTION_EXCHANGE,
                    queue: EventConstants.CREATE_PRODUCTION_QUEUE,
                    routingKey: string.Empty);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Houve um erro ao criar a conexão com o RabbitMQ. Erro: {ex.Message}");
            }
        }

        private async Task ProcessEventAsync(object sender, BasicDeliverEventArgs e)
        {
            using var scope = _serviceProvider.CreateScope();
            var value = Encoding.UTF8.GetString(e.Body.ToArray());
            try
            {
                var model = JsonConvert.DeserializeObject<CreateProductionModel>(value);

                var service = scope.ServiceProvider.GetRequiredService<ICreateProductionService>();

                await service.ProcessEventAsync(model);

                _channel.BasicAck(e.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _channel.BasicNack(e.DeliveryTag, false, false);
            }
            finally
            {
                scope.Dispose();
            }
        }
    }
}
