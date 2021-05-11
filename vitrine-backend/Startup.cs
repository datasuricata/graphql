using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vitrine_backend.Background;
using vitrine_backend.Background.Publishers;
using vitrine_backend.Infraestrutura;
using vitrine_backend.Trace;

namespace vitrine_backend
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
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();

            services.AddGraphQLServer()
               .AddQueryType<Query>()
               .AddMutationType<Mutation>()
               .AddDiagnosticEventListener(sp => new QueryLogger(sp.GetApplicationService<IBackgroundQueue<Log>>()));

            services
              .AddHostedService<BackgroundWorker>()
              .AddSingleton<IBackgroundQueue<Log>, BackgroundQueue<Log>>();

            services.AddScoped<ILoggerPublisher, LoggerPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseRouting();

            app.UseEndpoints(e => e.MapGraphQL());
        }
    }
}
