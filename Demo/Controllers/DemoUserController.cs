namespace Demo.Controllers
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Demo.Database;
  using Demo.Entity;
  using Demo.Services;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Logging;

  [ApiController]
  [Route("[controller]")]
  public class DemoUserController : DemoControllerBase<DemoDbContext>
  {
    private readonly ILogger<DemoUserController> logger;
    private readonly DemoUserService demoUserService;

    public DemoUserController(ILogger<DemoUserController> logger, IDbContextFactory<DemoDbContext> dbContextFactory, DemoUserService demoUserService)
      : base(dbContextFactory)
    {
      this.logger = logger;
      this.demoUserService = demoUserService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DemoUser>>> Get()
    {
      return Ok(await SlaveDbContext.DemoUsers.ToListAsync());
    }
  }
}
