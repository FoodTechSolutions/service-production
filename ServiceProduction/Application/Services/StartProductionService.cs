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
    public class StartProductionService(
        ILogger<StartProductionService> logger, 
        IProductionRepository productionRepository) : IStartProductionService
    {
        public async Task ProcessEventAsync(StartProductionModel rquest)
        {
            try
            {
                var production = productionRepository.GetById(rquest.ProductionId);

                production
                    .NextStatus()
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
