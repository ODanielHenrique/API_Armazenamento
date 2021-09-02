using API_Armazenamento.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Produto
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
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("database")); //Adicione para configurar a API
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Armazenamento", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Armazenamento v1"));
            }

            var scope = app.ApplicationServices.CreateScope();              //Adicione para configurar a API
            var service = scope.ServiceProvider.GetService<ApiContext>();   //Adicione para configurar a API
            var context = service;                                          //Adicione para configurar a API

            AdicionarDadosTeste(context); //Adicione para configurar a API

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            static void AdicionarDadosTeste(ApiContext context) //Adicione para configurar a API
            {
                var testeProdutos = new API_Armazenamento.Models.Produto
                {
                    Id = 1,
                    Codigo = 2691,
                    Descricao = "Chocolate",
                    Estoque = 50,
                    Preco = 1.55m
            };
                context.Produtos.Add(testeProdutos);
                context.SaveChanges();
            }
        }
    }
}
