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
    public class FinishProductionService(IProductionRepository productionRepository, ILogger<FinishProductionService> logger) : IFinishProductionService
    {
        public async Task ProcessEvent(FinishProductionModel model)
        {
            try
            {
                var production = productionRepository.GetById(model.ProductionId);

                if (production == null) throw new Exception("[FinishProductionService:ProcessEvent] Production not found: " + model.ProductionId);

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
