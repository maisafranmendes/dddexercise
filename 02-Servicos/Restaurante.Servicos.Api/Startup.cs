using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Infra.IoC;
using Restaurante.Aplicacao;
using AutoMapper;
using Restaurante.Infra.Data.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Servico.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Contexto>(o => o.UseMySql(Configuration.GetConnectionString("Restaurante")));
            InjetorDependencias.Registrar(services);
            services.AddAutoMapper(x => x.AddProfile(new MappingEntidade()));
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseMvc();
        }
    }
}