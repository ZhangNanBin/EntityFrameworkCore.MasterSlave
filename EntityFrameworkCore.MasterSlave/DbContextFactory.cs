namespace EntityFrameworkCore.MasterSlave
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;
  using EntityFrameworkCore.MasterSlave.Database;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.ChangeTracking;


  public class DbContextFactory<TDbContext> : IDbContextFactory<TDbContext>
     where TDbContext : MasterSlaveDbContext
  {
    private readonly TDbContext dbContext;

    public DbContextFactory(TDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public TDbContext SlaveDbContext
    {
      get
      {
        return (TDbContext)dbContext.SlaveDbContext();
      }
    }

    public TDbContext MasterDbContext
    {
      get
      {
        return (TDbContext)dbContext.MasterDbContext();
      }
    }

    public void Dispose()
    {
      SlaveDbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
      await SlaveDbContext.DisposeAsync();
    }

    #region 写入
    public int SaveChanges(bool acceptAllChangesOnSuccess)
    {
      return MasterDbContext.SaveChanges(acceptAllChangesOnSuccess);
    }

    public int SaveChanges()
    {
      return MasterDbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
      return await MasterDbContext.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      return await MasterDbContext.SaveChangesAsync(cancellationToken);
    }

    public EntityEntry Add(object entity)
    {
      return MasterDbContext.Add(entity);
    }

    public EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class
    {
      return MasterDbContext.Add(entity);
    }

    public async ValueTask<EntityEntry> AddAsync(object entity, CancellationToken cancellationToken = default)
    {
      return await MasterDbContext.AddAsync(entity, cancellationToken);
    }

    public async ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
      where TEntity : class
    {
      return await MasterDbContext.AddAsync(entity, cancellationToken);
    }

    public void AddRange(params object[] entities)
    {
      MasterDbContext.AddRange(entities);
    }

    public void AddRange(IEnumerable<object> entities)
    {
      MasterDbContext.AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default)
    {
      await MasterDbContext.AddRangeAsync(entities, cancellationToken);
    }

    public async Task AddRangeAsync(params object[] entities)
    {
      await MasterDbContext.AddRangeAsync(entities);
    }

    public EntityEntry Remove(object entity)
    {
      return MasterDbContext.Remove(entity);
    }

    public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class
    {
      return MasterDbContext.Remove(entity);
    }

    public void RemoveRange(IEnumerable<object> entities)
    {
      MasterDbContext.RemoveRange(entities);
    }

    public void RemoveRange(params object[] entities)
    {
      MasterDbContext.RemoveRange(entities);
    }

    public EntityEntry Update(object entity)
    {
      return MasterDbContext.Update(entity);
    }

    public EntityEntry<TEntity> Update<TEntity>(TEntity entity)
      where TEntity : class
    {
      return MasterDbContext.Update(entity);
    }

    public void UpdateRange(IEnumerable<object> entities)
    {
      MasterDbContext.UpdateRange(entities);
    }

    public void UpdateRange(params object[] entities)
    {
      MasterDbContext.UpdateRange(entities);
    }
    #endregion

    #region 读取
    public EntityEntry Attach(object entity)
    {
      return SlaveDbContext.Attach(entity);
    }

    public EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class
    {
      return SlaveDbContext.Attach(entity);
    }

    public void AttachRange(IEnumerable<object> entities)
    {
      SlaveDbContext.AttachRange(entities);
    }

    public void AttachRange(params object[] entities)
    {
      SlaveDbContext.AttachRange(entities);
    }

    [Obsolete]
    public DbQuery<TQuery> Query<TQuery>() where TQuery : class
    {
      return SlaveDbContext.Query<TQuery>();
    }

    public EntityEntry Entry(object entity)
    {
      return SlaveDbContext.Entry(entity);
    }

    public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
    {
      return SlaveDbContext.Entry(entity);
    }

    public TEntity Find<TEntity>(params object[] keyValues) where TEntity : class
    {
      return SlaveDbContext.Find<TEntity>(keyValues);
    }

    public object Find(Type entityType, params object[] keyValues)
    {
      return SlaveDbContext.Find(entityType, keyValues);
    }

    public async ValueTask<object> FindAsync(Type entityType, object[] keyValues, CancellationToken cancellationToken)
    {
      return await SlaveDbContext.FindAsync(entityType, keyValues, cancellationToken);
    }

    public async ValueTask<object> FindAsync(Type entityType, params object[] keyValues)
    {
      return await SlaveDbContext.FindAsync(entityType, keyValues);
    }

    public async ValueTask<TEntity> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken)
      where TEntity : class
    {
      return await SlaveDbContext.FindAsync<TEntity>(keyValues, cancellationToken);
    }

    public async ValueTask<TEntity> FindAsync<TEntity>(params object[] keyValues) where TEntity : class
    {
      return await SlaveDbContext.FindAsync<TEntity>(keyValues);
    }
    #endregion
  }
}
