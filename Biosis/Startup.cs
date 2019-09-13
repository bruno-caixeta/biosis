using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biosis.BusinessLayer.Implementation;
using Biosis.BusinessLayer.Interface;
using Biosis.Model;
using Biosis.Model.Repository.Implementation;
using Biosis.Model.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biosis
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
            services.AddCors(options =>
            {
                options.AddPolicy("fiver",
                    policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });
            services.AddDbContext<DataContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IResearchRepository, ResearchRepository>();
            services.AddScoped<IResearchBusinessLayer, ResearchBusinessLayer>();
            services.AddScoped<ITransDataRepository, TransDataRepository>();
            services.AddScoped<IAnalysisDataExtract, AnalysisDataExtract>();
            services.AddScoped<ITransCalculations, TransCalculations>();
            services.AddScoped<ITransDataBusinessLayer, TransDataBusinessLayer>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseCors("fiver");            
            app.UseMvc();
        }
    }
}
