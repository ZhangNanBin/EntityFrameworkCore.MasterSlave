using Demo.Database;
using Demo.Services;
using EntityFrameworkCore.MasterSlave;
using EntityFrameworkCore.MasterSlave.DbContext;
using EntityFrameworkCore.MasterSlave.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.Configure<DbConnectionOption>(Configuration.GetSection("ConnectionStrings"));//注入多个链接
      services.AddScoped<DemoDbContext>();
      services.AddScoped<DemoUserService>();
      services.AddScoped<IDbContextFactory<DemoDbContext>, DbContextFactory<DemoDbContext>>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
