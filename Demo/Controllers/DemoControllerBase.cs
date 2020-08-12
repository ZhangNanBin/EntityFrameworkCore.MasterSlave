namespace Demo.Controllers
{
  using Demo.Database;
  using EntityFrameworkCore.MasterSlave.Database;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using Microsoft.AspNetCore.Mvc;

  public class DemoControllerBase<TDbContext> : ControllerBase
    where TDbContext : MasterSlaveDbContext
  {
    public DemoControllerBase(IDbContextFactory<TDbContext> dbContextFactory)
    {
      DbContextFactory = dbContextFactory;
    }

    public IDbContextFactory<TDbContext> DbContextFactory { get; }

    public TDbContext MasterDbContext { get => DbContextFactory.MasterDbContext; }

    public TDbContext SlaveDbContext { get => DbContextFactory.SlaveDbContext; }
  }

  public class DemoControllerBase : DemoControllerBase<DemoDbContext>
  {

    public DemoControllerBase(IDbContextFactory<DemoDbContext> dbContextFactory)
     : base(dbContextFactory)
    {
    }
  }
}
