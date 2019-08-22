using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Commands;
using Liga.DataAccess;
using Liga.EfCommands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //DbContext
            services.AddDbContext<LigaContext>();

            //referee
            services.AddTransient<IGetRefereeCommand, EFGetRefereeCommand>();
            services.AddTransient<IGetRefereesCommand, EfGetRefereesCommand>();
            services.AddTransient<IEditRefereeCommand, EfEditRefereeCommand>();
            services.AddTransient<IDeleteRefereeCommand, EfDeleteRefereeCommand>();
            services.AddTransient<IAddRefereeCommand, EfAddRefereeCommand>();

            //position
            services.AddTransient<IGetPositionCommand, EfGetPositionCommand>();
            services.AddTransient<IGetPositionsCommand, EfGetPositionsCommand>();
            services.AddTransient<IEditPositionCommand, EfEditPositionCommand>();
            services.AddTransient<IDeletePositionCommand, EfDeletePositionCommand>();
            services.AddTransient<IAddPositionCommand, EfAddPositionCommand>();

            //player 
            services.AddTransient<IGetPlayerCommand, EfGetPlayerCommand>();
            services.AddTransient<IGetPlayersCommand, EfGetPlayersCommand>();
            services.AddTransient<IEditPlayerCommand, EfEditPlayerCommand>();
            services.AddTransient<IDeletePlayerCommand, EfDeletePlayerCommand>();
            services.AddTransient<IAddPlayerCommand, EfAddPlayerCommand>();

            //league
            services.AddTransient<IGetLeagueCommand, EfGetLeagueCommand>();
            services.AddTransient<IGetLeaguesCommand, EfGetLeaguesCommand>();
            services.AddTransient<IEditLeagueCommand, EfEditLeagueCommand>();
            services.AddTransient<IDeleteLeagueCommand, EfDeleteLeagueCommand>();
            services.AddTransient<IAddLeagueCommand, EfAddLeagueCommand>();

            //club

            services.AddTransient<IGetClubCommand, EfGetClubCommand>();
            services.AddTransient<IGetClubsCommand, EfGetClubsCommand>();
            services.AddTransient<IEditClubCommand, EfEditClubCommand>();
            services.AddTransient<IDeleteClubCommand, EfDeleteClubCommand>();
            services.AddTransient<IAddClubCommand, EfAddClubCommand>();

            services.AddTransient<IGetCityCommand, EfGetCityCommand>();
            services.AddTransient<IGetCitiesCommand, EfGetCitiesCommand>();
            services.AddTransient<IEditCityCommand, EfEditCityCommand>();
            services.AddTransient<IDeleteCityCommand, EfDeleteCityCommand>();
            services.AddTransient<IAddCityCommand, EfAddCityCommand>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fudbalska Liga Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                
               
            });
        }
    }
}
