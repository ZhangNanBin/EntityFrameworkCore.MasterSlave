namespace Demo.Controllers
{
  using Demo.Database;
  using EntityFrameworkCore.MasterSlave.Database;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Extensions.Logging;

  public class DemoControllerBase<TController, TDbContext> : ControllerBase
    where TController : ControllerBase
    where TDbContext : MasterSlaveDbContext
  {
    private readonly IDbContextFactory<TDbContext> dbContextFactory;


    public DemoControllerBase(IDbContextFactory<TDbContext> dbContextFactory)
      : this(dbContextFactory, null)
    {
    }

    public DemoControllerBase(ILogger<TController> logger)
      : this(null, logger)
    {
    }

    public DemoControllerBase(IDbContextFactory<TDbContext> unitOfWork, ILogger<TController> logger)
    {
      this.dbContextFactory = unitOfWork;
      Logger = logger;
    }

    public TDbContext MasterDbContext { get => dbContextFactory.MasterDbContext; }

    public TDbContext SlaveDbContext { get => dbContextFactory.SlaveDbContext; }

    public ILogger<TController> Logger { get; set; }
  }

  public class DemoControllerBase : DemoControllerBase<DemoControllerBase, DemoDbContext>
  {

    public DemoControllerBase(IDbContextFactory<DemoDbContext> dbContextFactory)
     : base(dbContextFactory)
    {
    }

    public DemoControllerBase(ILogger<DemoControllerBase> logger)
      : base(logger)
    {
    }

    public DemoControllerBase(IDbContextFactory<DemoDbContext> dbContextFactory, ILogger<DemoControllerBase> logger) : base(dbContextFactory, logger)
    {
    }
  }

  public class DemoControllerBase<TDbContext> : DemoControllerBase<DemoControllerBase, TDbContext>
    where TDbContext : MasterSlaveDbContext
  {

    public DemoControllerBase(IDbContextFactory<TDbContext> dbContextFactory)
     : base(dbContextFactory)
    {
    }

    public DemoControllerBase(ILogger<DemoControllerBase> logger)
      : base(logger)
    {
    }

    public DemoControllerBase(IDbContextFactory<TDbContext> dbContextFactory, ILogger<DemoControllerBase> logger) : base(dbContextFactory, logger)
    {
    }
  }
}
