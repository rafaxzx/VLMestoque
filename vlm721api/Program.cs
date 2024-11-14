using Microsoft.AspNetCore.Connections;
using System.Data;
using System.Data.SQLite;
using vlm721api.Repositories;

namespace vlm721api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>(); 

            var stringConnection = configuration.GetConnectionString("SQLiteConnection");

            builder.Services.AddScoped<SQLiteConnection>((connection) => new SQLiteConnection(stringConnection));

            using (var connection = new SQLiteConnection(stringConnection))
            {
                connection.Open();
                string sql = @"CREATE TABLE IF NOT EXISTS Manufacturers (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Manufacturername TEXT,
                                Manufacturerimagepath TEXT);";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                sql = 
                    @"CREATE TABLE IF NOT EXISTS Items
                    (Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Codeintern TEXT,
                    Itemname TEXT,
                    Itemspecification TEXT,
                    Manufacturerid INTEGER,
                    UDCnumber INTEGER,
                    Locationtray TEXT,
                    Minstock INTEGER,
                    Maxstock INTEGER);";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
