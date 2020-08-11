namespace EntityFrameworkCore.MasterSlave
{
  using EntityFrameworkCore.MasterSlave.DbContext;
  using EntityFrameworkCore.MasterSlave.Interfaces;

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
  }
}
