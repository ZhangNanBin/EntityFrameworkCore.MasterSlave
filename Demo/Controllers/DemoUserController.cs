﻿namespace Demo.Controllers
{
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using Demo.Database;
  using Demo.Entity;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;

  [ApiController]
  [Route("[controller]")]
  public class DemoUserController : DemoControllerBase<DemoDbContext>
  {
    public DemoUserController(IDbContextFactory<DemoDbContext> dbContextFactory)
      : base(dbContextFactory)
    {
    }

    [HttpGet]
    public async Task<ActionResult<List<DemoUser>>> Get()
    {
      var Db = SlaveDbContext;
      return Ok(new { Data = await Db.DemoUsers.ToListAsync(), Db.Database.GetDbConnection().ConnectionString });
    }

    [HttpPost]
    public async Task<ActionResult<List<DemoUser>>> Create()
    {
      var data = DbContextFactory.Add(new DemoUser() { Name = "Test", Password = "123123", Email = "test@qq.com", Mobile = "1008611" });
      await DbContextFactory.SaveChangesAsync();
      return Ok(new { Data = data.Entity, MasterDbContext.Database.GetDbConnection().ConnectionString });
    }
  }
}
