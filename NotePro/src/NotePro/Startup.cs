using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotePro.Models;
using NotePro.Data;

namespace NotePro
{
    public class Startup
    {
        //test
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddMvc();
            services.AddDbContext<NoteProContext>(opt => opt.UseInMemoryDatabase());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                var context = app.ApplicationServices.GetService<NoteProContext>();
                AddTestData(context);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AddTestData(NoteProContext context)
        {
            var author = new AuthorNew
            {
                FirstName = "Martin",
                LastName = "Meier"
            };

            context.AuthorsNew.Add(author);

            var note = new Note
            {
                Id = 1,
                Title = "Einkauf1",
                Description = "Milch einkaufen",
                Importance = 2,
                DueDate = new DateTime(2016, 9, 18),
                AuthorId = author.Id,
                Finished = false
            };
            var note2 = new Note
            {
                Id = 2,
                Title = "Einkauf2",
                Description = "Brot einkaufen",
                Importance = 3,
                DueDate = new DateTime(2016, 9, 18),
                AuthorId = author.Id,
                Finished = true
            };
            var note3 = new Note
            {
                Id = 3,
                Title = "Einkauf3",
                Description = "Brot einkaufen",
                Importance = 2,
                DueDate = new DateTime(2016, 9, 18),
                AuthorId = author.Id,
                Finished = true
            };

            context.Notes.Add(note);
            context.Notes.Add(note2);
            context.Notes.Add(note3);
            context.SaveChanges();
        }
    }
}
