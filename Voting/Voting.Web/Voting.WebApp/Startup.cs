using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Voiting.Repositories;
using Voiting.Repositories.Interfaces;
using Voiting.Repositories.Repositories;
using Voting.Logic;
using Voting.Logic.Interfaces;
using Voting.Logic.Services;
using VueCliMiddleware;

namespace Voting.WebApp
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
      services.AddControllers();
      services.AddSpaStaticFiles(configuration =>
      {
        configuration.RootPath = "ClientApp";
      });
            
      services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
          options.Events.OnRedirectToAccessDenied = context =>
          {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
          };
          options.Events.OnRedirectToLogin = context =>
          {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
          };
        });
      
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MappingProfile());
      });

      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddSingleton(typeof(DbConnectionSettings), c => new DbConnectionSettings
      {
        ConnectionString = Configuration["Database:ConnectionString"],
        CommandTimeout = Convert.ToInt32(Configuration["Database:CommandTimeout"])
      });

      services.AddTransient<IDbUsersRepository, DbUsersRepository>();
      services.AddTransient<IDbQuestionsRepository, DbQuestionsRepository>();
      services.AddTransient<IDbAnswersRepository, DbAnswersRepository>();
      services.AddTransient<IDbVotesRepository, DbVotesRepository>();

      services.AddTransient<IAccountService, AccountService>();
      services.AddTransient<IQuestionsService, QuestionsService>();
      services.AddTransient<IAnswersService, AnswersService>();
      services.AddTransient<IVotesService, VotesService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseSpaStaticFiles();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSpa(spa =>
      {
        if (env.IsDevelopment())
          spa.Options.SourcePath = "ClientApp";
        else
          spa.Options.SourcePath = "dist";

        if (env.IsDevelopment())
        {
          spa.UseVueCli(npmScript: "serve");
        }

      });
    }
  }
}
