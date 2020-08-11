namespace EntityFrameworkCore.MasterSlave.Interfaces
{
  using EntityFrameworkCore.MasterSlave.DbContext;

  public interface IDbContextFactory<TDbContext>
    where TDbContext : MasterSlaveDbContext
  {
    TDbContext SlaveDbContext { get; }
    TDbContext MasterDbContext { get; }
  }
}