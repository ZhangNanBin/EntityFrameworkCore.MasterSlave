namespace EntityFrameworkCore.MasterSlave.Database
{
  using System;
  using EntityFrameworkCore.MasterSlave.Utils;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Options;

  public class MasterSlaveDbContext : DbContext
  {
    private readonly DbConnectionOption dbConnectionOption = null;

    public MasterSlaveDbContext(IOptionsMonitor<DbConnectionOption> options)
    {
      if (options is null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      dbConnectionOption = options.CurrentValue;
    }

    public virtual MasterSlaveDbContext MasterDbContext()
    {
      // 未开启读写分离直接返回this对象
      if (dbConnectionOption.CQRSEnabled)
      {
        Database.GetDbConnection().ConnectionString = this.dbConnectionOption.MasterConnectionConfig;
      }

      return this;
    }

    public virtual MasterSlaveDbContext SlaveDbContext()
    {
      // 未开启读写分离直接返回this对象
      if (dbConnectionOption.CQRSEnabled)
      {
        var connectionString = UtilRandom.GetConnectionString(dbConnectionOption);
        Database.GetDbConnection().ConnectionString = connectionString;
      }

      return this;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // 使用不同的数据库提供程序
      var masterConnectionConfig = dbConnectionOption.MasterConnectionConfig;
      if (dbConnectionOption.DbType == DbType.MySQL)
      {
        optionsBuilder.UseMySql(masterConnectionConfig);
      }
      else if (dbConnectionOption.DbType == DbType.SqlServer)
      {
        optionsBuilder.UseSqlServer(masterConnectionConfig);
      }
      else if (dbConnectionOption.DbType == DbType.Sqlite)
      {
        optionsBuilder.UseSqlite(masterConnectionConfig);
      }
      else if (dbConnectionOption.DbType == DbType.PostgreSQL)
      {
        optionsBuilder.UseNpgsql(masterConnectionConfig);
      }
    }
  }
}
