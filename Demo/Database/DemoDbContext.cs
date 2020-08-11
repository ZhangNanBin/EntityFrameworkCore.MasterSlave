namespace Demo.Database
{
  using Demo.Entity;
  using EntityFrameworkCore.MasterSlave.DbContext;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Options;

  public class DemoDbContext : MasterSlaveDbContext
  {
    public DemoDbContext(IOptionsMonitor<DbConnectionOption> options)
      : base(options)
    {
    }

    public virtual DbSet<DemoUser> DemoUsers { get; set; }
  }
}
