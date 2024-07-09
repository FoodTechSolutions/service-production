using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common;

public abstract class RepositoryBase<TEntity> : IDisposable where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;
    private readonly ProductionContext _context;

    protected RepositoryBase(ProductionContext context)
    {
        _context = context;
        DbSet = context.Set<TEntity>();
    }

    #region IDisposable

    private bool _disposed;

    ~RepositoryBase() => Dispose(false);

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Dispose managed state (managed objects).
        if (disposing)
            _context.Dispose();

        _disposed = true;
    }

    #endregion

}