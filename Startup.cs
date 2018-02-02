using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CursosLivre.Dados;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace CursosLivre
{
    public class Startup
    {
        IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CursosLivreContexto>(opt => opt.UseSqlServer(Configuration.GetConnectionString("BancoCursosLivre")));
            services.AddMvc();

            //adicionando o Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    //dados do arquivo
                    Version = "v1",
                    Title = "Cursos Livre",
                    Description = "Teste Simples",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Gabriela Giraud", Email = "giraudev@gmail.com", Url = "http:///github.com/giraudev" }
                });

                //caminho onde está a documentação
                var basePath = AppContext.BaseDirectory;
                //pegar os comentários no arquivo .xml
                var xmlPath = Path.Combine(basePath, "CursosLivre.xml");

                //comando para incluir os comentários dentro do arquivo
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

            app.UseMvc();

            //código para utilizar o Swagger
            app.UseSwagger();

            //UI: user interface
            app.UseSwaggerUI(c =>
            {
                //para o usuário acessar a documentação, definir a url
                //"My API V1" é a descrição
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Você saiu da rota");
            });
        }
    }
}
