using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductionRepository : EfRepository<Production>, IProductionRepository
{
    public DbSet<Production> Dbset { get; set; }
    public ProductionRepository(ProductionContext context) : base(context)
    {
        Dbset = context.Set<Production>();
    }

    public Production GetOrderId(Guid orderId)
    {
        return DbSet.FirstOrDefault(x => x.Order == orderId.ToString());
    }
}