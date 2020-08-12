namespace Demo
{
  using Demo.Database;
  using Demo.Middleware;
  using EntityFrameworkCore.MasterSlave;
  using Microsoft.AspNetCore.Builder;
  using Microsoft.AspNetCore.Hosting;
  using Microsoft.Extensions.Configuration;
  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.Extensions.Hosting;

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
      services.AddMasterSlave<DemoDbContext>(Configuration.GetSection("ConnectionStrings"));
      services.AddCors(options =>
      {

        options.AddPolicy("zhang", policy =>
         {
           policy.WithOrigins("https://localhost:5005")
               .AllowAnyHeader()
               .AllowAnyMethod();
         });
      });
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

      app.UseMiddleware<CorsMiddleware>("zhang");

      app.UseAuthorization();

      app.UseMiddleware<RequestIPMiddleware>();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

    }
  }
}
