using Domain.DTO;

namespace Domain.Services;

public interface IProductionService
{
    Result ReceiveOrder(ProductionDto model);
    Result StartProduction(Guid productionId);
    Result CancelProduction(Guid productionId);
    Result FinishProduction(Guid productionId);
}