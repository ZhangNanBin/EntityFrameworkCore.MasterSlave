namespace EntityFrameworkCore.MasterSlave
{
  using System;
  using System.Linq;
  using EntityFrameworkCore.MasterSlave.Database;
  using EntityFrameworkCore.MasterSlave.Interfaces;
  using EntityFrameworkCore.MasterSlave.Utils;
  using Microsoft.EntityFrameworkCore;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Logging;

  public static class MasterSlaveExtensions
  {
    public static IServiceCollection AddMasterSlave<TDbContext>(this IServiceCollection services)
      where TDbContext : MasterSlaveDbContext
    {
      IConfiguration configuration = services.GetSingletonInstanceOrNull<IConfiguration>();

      var section = configuration.GetSection("ConnectionStrings");
      var dbConnectionOption = section.Get<DbConnectionOption>();

      services.Configure<DbConnectionOption>(section); //注入多个链接
      services.AddScoped<TDbContext>(); // 注入TDbContext
      services.AddScoped<DbContext, TDbContext>(); // 使用TDbContext替换DbContext
      services.AddScoped<IDbContextFactory<TDbContext>, DbContextFactory<TDbContext>>();

      Console.WriteLine($"使用数据库：{Util.GetEnumDescription(dbConnectionOption.DbType)}");
      if (dbConnectionOption.CQRSEnabled)
      {
        Console.WriteLine($"使用轮询策略：{Util.GetEnumDescription(dbConnectionOption.RoundRobinPolicy)}");
      }
      return services;
    }

    /// <summary>
    /// 获取单例注册服务对象
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="services">依赖注入服务容器</param>
    /// <returns>数据</returns>
    public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
    {
      ServiceDescriptor descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Singleton);

      if (descriptor?.ImplementationInstance != null)
      {
        return (T)descriptor.ImplementationInstance;
      }

      if (descriptor?.ImplementationFactory != null)
      {
        return (T)descriptor.ImplementationFactory.Invoke(null);
      }

      return default;
    }
  }
}
