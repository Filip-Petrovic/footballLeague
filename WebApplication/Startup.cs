using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands;
using Liga.DataAccess;
using Liga.EfCommands;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


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

            //city
            services.AddTransient<IGetCityCommand, EfGetCityCommand>();
            services.AddTransient<IGetCitiesCommand, EfGetCitiesCommand>();
            services.AddTransient<IEditCityCommand, EfEditCityCommand>();
            services.AddTransient<IDeleteCityCommand, EfDeleteCityCommand>();
            services.AddTransient<IAddCityCommand, EfAddCityCommand>();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
