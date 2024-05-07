using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuestionsAndAnswers.DataLayer.Models;
using QuestionsAndAnswers.DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjectAPI.Services;
using ProjectAPI.Helpers;

namespace ProjectAPI
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
            services.AddDbContext<ChatBotContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            services.AddScoped<IUserService, UserService>();
            services.AddControllers();

            services.AddAutoMapper(typeof(UserProfile));
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll", policy =>
                {

                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            /*services.AddCors(x =>
            {
                x.AddPolicy(name: "AllowAll", builder =>
                {
                    builder.WithOrigins("http://10.116.24.6:3000/html/Index.html")
                    .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                    .AllowAnyHeader().AllowCredentials();

                //builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials();

            });
            });*/

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Цымбал А.В.");
            });


            app.UseRouting();
            
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
