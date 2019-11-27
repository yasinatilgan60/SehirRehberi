using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SehirRehberi_API.Data;
using SehirRehberi_API.Helpers;

namespace SehirRehberi_API
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
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value); // gizli anahtar şifrelendi.
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings")); // CloudinarySettings'e map işlemi gerçekleştirildi.
            // DataContext istendiğinde sql server kullanılarak default connection içerisindeki db ile bağlantı kuruldu.
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMvc().AddJsonOptions(opt=>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Sonsuz referans döngüsüne girerse ignore edecektir.
            });
            services.AddCors();
            services.AddScoped<IAppRepository, AppRepository>(); // Dependency Injection, Controller'da IAppRepository istendiğinde AppRepository kullanılacaktır.
            services.AddScoped<IAuthRepository, AuthRepository>(); //  Dependency Injection, Controller'da IAuthRepository istendiğinde AuthRepository kullanılacaktır.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
            {
                // jwt yapısı;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, // Anahtar eklenecek.
                    IssuerSigningKey = new SymmetricSecurityKey(key), // appsettings.json içerisine eklenecek. (simetrik anahtar)
                    ValidateIssuer = false, // proje public olduğu için false verildi.
                    ValidateAudience = false // proje public olduğu için false verildi.
                };
            }); // Extension helpers'a yazıldı.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Gelen her isteği kabul etmek için; (Tarayıcalara güvenlik konusunda bildirimde bulunur.)
            app.UseCors(x=> x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
