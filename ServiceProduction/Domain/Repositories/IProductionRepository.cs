using Domain.Entities;
using Domain.Repositories.Common;

namespace Domain.Repositories;

public interface IProductionRepository : IAsyncRepository<Production>
{
    Production GetOrderId(Guid orderId);
}