using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Domain.Entities;
using Domain.Repositories.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common;

public abstract class EfRepository<TEntity> : RepositoryBase<TEntity>, IAsyncRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly ProductionContext _context;

    protected EfRepository(ProductionContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbSet.Where(x => x.DeletedAt == null);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query.OrderBy(x => x.UpdatedAt).ToListAsync().Result;
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
        SaveChanges();
    }

    public void AddRange(IEnumerable<TEntity> entities) =>
        DbSet.AddRange(entities);

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
        SaveChanges();
    }

    public void UpdateRange(IEnumerable<TEntity> entities) =>
        DbSet.UpdateRange(entities);

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
        SaveChanges();
    }

    public void RemoveRange(IEnumerable<TEntity> entities) =>
        DbSet.RemoveRange(entities);

    private void SaveChanges()
    {
        _context.SaveChanges();
    }

    public TEntity? GetById(Guid id)
    {
        return DbSet.FirstOrDefault(e => e.Id == id);
    }
}