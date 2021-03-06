﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Clients;
using Movies.Clients.Interfaces;
using Movies.Contexts;
using Movies.Contexts.Interfaces;
using Movies.Mappers;
using Movies.Mappers.Interfaces;
using Movies.Repositories;
using Movies.Repositories.Interfaces;
using Movies.Services;
using Movies.Services.Interfaces;

namespace Movies
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors();

            services.AddSwaggerGen();

            var omdbBaseUrl = Configuration["OmdbApiUrl:Url"];

            var tmdbBaseUrl = Configuration["TmdbApi:Url"];

            var tmdbToken = Configuration["TmdbApi:Token"];

            var connectionString = Configuration["MoviesDb:ConnectionString"];

            services
                .AddSingleton<IOmdbClient>(new OmdbClient(omdbBaseUrl))
                .AddSingleton<ITmdbClient>(new TmdbClient(tmdbBaseUrl, tmdbToken))
                .AddSingleton<IMoviesService, MoviesService>()
                .AddSingleton<IGenresService, GenresService>()
                .AddSingleton<IMoviesRequestsCache, MoviesRequestsCache>()
                .AddSingleton<IGenresMapper, GenresMapper>()
                .AddSingleton<IMoviesMapper, MoviesMapper>()
                .AddSingleton<IMoviesDbContextFactory>(new MoviesDbContextFactory(connectionString))
                .AddSingleton<IMoviesRepository, MoviesRepository>()
                .AddDbContext<MoviesDbContext>(options => options.UseSqlite(connectionString));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUi();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        }
    }
}
