using Domain.DTO;

namespace Domain.Services;

public interface IProductionService
{
    Result ReceiveOrder(ReceivingOrderDto model);
    Result StartProduction(Guid productionId);
    Result CancelProduction(Guid productionId);
    Result FinishProduction(Guid productionId);
}