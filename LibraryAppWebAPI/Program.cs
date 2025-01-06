
using LibraryAppWebAPI.Data;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //var contextOptions = new DbContextOptionsBuilder<LibraryDbContext>()
            //    .UseSqlServer(builder.Configuration.GetConnectionString("BooksDb"))
            //    .Options;

            builder.Services.AddDbContext<LibraryDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("BooksDb")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                db.Database.EnsureCreated();
                db.Database.EnsureDeleted();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
