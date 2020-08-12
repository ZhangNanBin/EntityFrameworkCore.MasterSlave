namespace EntityFrameworkCore.MasterSlave
{
  using System;
  using EntityFrameworkCore.MasterSlave.Database;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using EntityFrameworkCore.MasterSlave.Utils;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;

  public static class MasterSlaveExtensions
  {
    public static IServiceCollection AddMasterSlave<TDbContext>(this IServiceCollection services, IConfigurationSection configurationSection)
      where TDbContext : MasterSlaveDbContext
    {
      var dbConnectionOption = configurationSection.Get<DbConnectionOption>();

      services.Configure<DbConnectionOption>(configurationSection); //注入多个链接
      services.AddScoped<TDbContext>(); // 注入TDbContext
      // services.AddScoped<DbContext, TDbContext>(); // 使用TDbContext替换DbContext
      services.AddScoped<IDbContextFactory<TDbContext>, DbContextFactory<TDbContext>>();

      Console.WriteLine($"使用数据库：{Util.GetEnumDescription(dbConnectionOption.DbType)}");
      if (dbConnectionOption.CQRSEnabled)
      {
        Console.WriteLine($"使用轮询策略：{Util.GetEnumDescription(dbConnectionOption.RoundRobinPolicy)}");
      }
      return services;
    }
  }
}
