
using LibraryAppWebAPI.Data;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace LibraryAppWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine($"Environment set to {env}");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var connectionString = builder.Configuration.GetConnectionString("BooksDb");

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Azure")
            {
                var connectionBuilder = new SqlConnectionStringBuilder(connectionString)
                {
                    Password = builder.Configuration["DbPassword"]
                };
                connectionString = connectionBuilder.ConnectionString;
            }

            builder.Services.AddDbContext<LibraryDbContext>(opt =>
                opt.UseSqlServer(connectionString)
                .LogTo(msg => Debug.WriteLine(msg)));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            //    db.Database.EnsureCreated();
            //    db.Database.EnsureDeleted();
            //}

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.CreateDbIfNotExists();

            app.Run();
        }
    }
}
