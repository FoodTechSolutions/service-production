using Application.BackgroundServices.Models;
using Application.Services.Interface;
using Domain.DTO;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CancelProductionService(ILogger<CancelProductionService> logger, IProductionRepository productionRepository) : ICancelProductionService
    {
        public async Task ProcessEventAsync(CancelProductionModel request)
        {
            try
            {
                var production = productionRepository.GetById(request.ProductionId);

                production
                    .CancelProduction()
                    .SetUpdatedAt();

                productionRepository.Update(production);
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message);
            }
        }
    }
}
