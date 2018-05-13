using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Todo1.Models;
using System.Threading;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure.Disposal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;

namespace Todo1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly AsyncLocal<Scope> scopeProvider = new AsyncLocal<Scope>();
        private IKernel Kernel { get; set; }

        private object Resolve(Type type) => Kernel.Get(type);
        private object RequestScope(IContext context) => scopeProvider.Value;

        private sealed class Scope : DisposableObject { }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var connection = @"Server=DESKTOP\SQLEXPRESS;Database=Todo;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(connection));
            services.AddTransient<ITodoService, TodoService>();
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddRequestScopingMiddleware(() => scopeProvider.Value = new Scope());
            //services.AddCustomControllerActivation(Resolve);
            //services.AddCustomViewComponentActivation(Resolve);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //this.Kernel = this.RegisterApplicationComponents(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
        //private IKernel RegisterApplicationComponents(IApplicationBuilder app)
        //{
        //    // IKernelConfiguration config = new KernelConfiguration();
        //    var kernel = new StandardKernel();

        //    // Register application services
        //    foreach (var ctrlType in app.GetControllerTypes())
        //    {
        //        kernel.Bind(ctrlType).ToSelf().InScope(RequestScope);
        //    }

        //    // This is where our bindings are configurated
        //    //kernel.Bind<ITestService>().To<TestService>().InScope(RequestScope);
        //    kernel.Bind<DbContext>().To<TodoContext>().InScope(RequestScope);

        //    // Cross-wire required framework services
        //    kernel.BindToMethod(app.GetRequestService<IViewBufferScope>);

        //    return kernel;
        //}

    }
}
