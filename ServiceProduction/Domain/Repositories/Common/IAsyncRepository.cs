using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Repositories.Common;

public interface IAsyncRepository<TEntity> : IRepository where TEntity : BaseEntity
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> GetAll();
    TEntity? GetById(Guid id);
}

public interface IRepository
{
}
