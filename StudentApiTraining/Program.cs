using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentApiTraining.Data;
using StudentApiTraining.Repositories;
using StudentApiTraining.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentApiTraining
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // When IStudentService is requested → provide StudentService
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();

            // When IStudentRepository is requested → provide StudentRepository
            builder.Services.AddScoped<IStudentService, StudentService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //Without this line, tools like Swagger won't be able to find the endpoints in the API and generate documentation for them.
            builder.Services.AddEndpointsApiExplorer();

            // This registers the Swagger documentation generator which automatically creates interactive API documentation.
            builder.Services.AddSwaggerGen();

            // This tells ASP.NET: to use SQL Server database and create ApplicationDbContext using this connection string
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //ASP.NET maps: "api/[controller]" → StudentsController
            app.MapControllers();

            app.Run();
        }
    }
}
