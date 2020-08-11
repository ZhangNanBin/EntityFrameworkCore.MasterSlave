namespace Demo.Services
{
  using Demo.Controllers;
  using Demo.Database;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  public class DemoUserService
  {
    private readonly ILogger<DemoUserController> logger;
    private readonly IDbContextFactory<DemoDbContext> unitOfWork;

    public DemoUserService(ILogger<DemoUserController> logger, IDbContextFactory<DemoDbContext> unitOfWork)
    {
      this.logger = logger;
      this.unitOfWork = unitOfWork;
    }

    public void Test()
    {
      logger.LogInformation("DemoUserService.MasterDbContext：" + unitOfWork.MasterDbContext.Database.GetDbConnection().ConnectionString);
      logger.LogInformation("DemoUserService.SlaveDbContext：" + unitOfWork.SlaveDbContext.Database.GetDbConnection().ConnectionString);
    }
  }
}
