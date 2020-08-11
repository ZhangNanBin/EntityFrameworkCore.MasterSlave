namespace EntityFrameworkCore.MasterSlave.DbContext
{
  using System;
  using EntityFrameworkCore.MasterSlave.Utils;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Options;

  public class MasterSlaveDbContext : DbContext
  {
    private readonly DbConnectionOption DbConnectionOption = null;

    public MasterSlaveDbContext(IOptionsMonitor<DbConnectionOption> options)
    {
      if (options is null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      DbConnectionOption = options.CurrentValue;
    }

    public virtual DbContext SlaveDbContext()
    {
      // 未开启读写分离直接返回this对象
      if (DbConnectionOption.CQRSEnabled)
      {
        var connectionString = UtilRandom.GetConnectionString(DbConnectionOption);
        Database.GetDbConnection().ConnectionString = connectionString;
      }

      return this;
    }

    public virtual DbContext MasterDbContext()
    {
      // 未开启读写分离直接返回this对象
      if (DbConnectionOption.CQRSEnabled)
      {
        Database.GetDbConnection().ConnectionString = this.DbConnectionOption.MasterConnectionConfig;
      }

      return this;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // 使用不同的数据库提供程序
      var masterConnectionConfig = DbConnectionOption.MasterConnectionConfig;
      if (DbConnectionOption.DbType == DbType.MySQL)
      {
        optionsBuilder.UseMySql(masterConnectionConfig);
      }
      else if (DbConnectionOption.DbType == DbType.SqlServer)
      {
        optionsBuilder.UseSqlServer(masterConnectionConfig);
      }
      else if (DbConnectionOption.DbType == DbType.Sqlite)
      {
        optionsBuilder.UseSqlite(masterConnectionConfig);
      }
      else if (DbConnectionOption.DbType == DbType.PostgreSQL)
      {
        optionsBuilder.UseNpgsql(masterConnectionConfig);
      }
    }
  }
}
