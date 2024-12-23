using Microsoft.EntityFrameworkCore;
using SkillSwapAPI;
using SkillSwapApi.Data;
using SkillSwapAPI.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;

namespace SkillSwapApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<AppDBCont>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddHttpClient();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll" ,policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            builder.Services.AddSingleton<MMService>();

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            };

            app.UseHttpsRedirection();
            /*app.UseCors(options =>
            {
                options.WithOrigins("https://localhost:5284")
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .AllowAnyHeader();


                //options.AllowAnyOrigin(); //for any IP Address (Use only for test)
                //options.WithMethods(); //for custom allows of Get Post Put Delete Methods!!!
                //options.WithOrigin(); //for custom IP Address
            });*/
            
            app.UseCors("AllowAll");
            app.UseAuthorization();

            app.MapHub<MMHub>("/mmhub");
            app.MapControllers();



            app.Run();
        }
    }
}