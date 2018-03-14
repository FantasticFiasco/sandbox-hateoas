using AutoMapper;
using Halcyon.Web.HAL.Json;
using Hateoas.Filters;
using Hateoas.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hateoas
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
            Mapper.Initialize(_ => { });

            services.AddSingleton<AuthorService>();
            services.AddSingleton<ArticleService>();
            services.AddSingleton<CommentService>();

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelState));

                options.OutputFormatters.RemoveType<JsonOutputFormatter>();
                options.OutputFormatters.Add(new JsonHalOutputFormatter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}