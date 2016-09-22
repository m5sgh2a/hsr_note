using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NotePro.Models;
using NotePro.Data;
using Microsoft.AspNetCore.Http;
using NotePro.Services;

namespace NotePro
{
    public class Startup
    {
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
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase());
            services.AddTransient<INoteService, NoteService>();
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

                var context = app.ApplicationServices.GetService<AppDbContext>();
                AddTestData(context);
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "CookieAuth",
                LoginPath = new PathString("/User/Login/"),
                AccessDeniedPath = new PathString("/User/Login/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static void AddTestData(AppDbContext context)
        {
            var author = new Register
            {
                FirstName = "Martin",
                LastName = "Meier",
                Email = "Martin.Meier@gmail.com",
                Password = "123456"
            };

            context.Register.Add(author);

            var note = new Note
            {
                Id = 1,
                CreateDate = DateTime.Now,
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
                CreateDate = DateTime.Now,
                Title = "Einkauf2",
                Description = "Brot einkaufen",
                Importance = 3,
                DueDate = new DateTime(2016, 9, 18),
                AuthorId = author.Id,
                FinishDate = DateTime.Now,
                Finished = true
            };
            var note3 = new Note
            {
                Id = 3,
                CreateDate = DateTime.Now,
                Title = "Einkauf3",
                Description = "Brot einkaufen",
                Importance = 2,
                DueDate = new DateTime(2016, 9, 18),
                AuthorId = author.Id,
                FinishDate = DateTime.Now,
                Finished = true
            };

            context.Notes.Add(note);
            context.Notes.Add(note2);
            context.Notes.Add(note3);
            context.SaveChanges();
        }
    }
}
