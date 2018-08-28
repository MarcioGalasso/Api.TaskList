using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharedKernel.DependencyInjector;
using SharedKernel.Domain.Services;
using SharedKernel.Domain.Services.Interface;
using SimpleInjector;
using Supero.TaskList.Maps;

namespace Supero.TaskList.Api
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
            services.AddMvc();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            });
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            Kernel.IntegrateAspNet(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
         
            app.UseMvc();
            Kernel.StartAspNet(Configuration, app);
            Kernel.GetKernel().Register<IConfiguration>(() => Configuration);
            BindIocSql();
        }

        public void BindIocSql()
        {
            var kernel = Kernel.GetKernel();
            kernel.Register<DbContext>(() => {
                var opBuilder = new DbContextOptionsBuilder<TaskListContext>();
                opBuilder.UseSqlServer(Configuration.GetConnectionString("TaskList"));
                return new TaskListContext(opBuilder.Options);
            },
                Lifestyle.Scoped);

            Kernel.Bind<IUsuarioService, UsuarioService>();
            Kernel.Bind<IApplicationUserService, ApplicationUserService>();
        }
    }
}
