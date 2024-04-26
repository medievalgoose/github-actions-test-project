using ClassTest.API.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MySql;

namespace ClassTest.IntegrationTest.Helpers
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        MySqlContainer _mysqlContainer = new MySqlBuilder()
            .WithImage("mysql:8.0")
            .WithUsername("root")
            .WithPassword("root")
            .WithDatabase("db_class_test")
            .Build();

        public async Task InitializeAsync()
        {
            await _mysqlContainer.StartAsync();

            var mysqlConnection = new MySqlConnection(_mysqlContainer.GetConnectionString());
            await mysqlConnection.OpenAsync();

            // Get filepath
            string sqlFilePath = Directory.GetCurrentDirectory() + "/Helpers/db_class_test.sql";

            // Convert it into string
            string commandText = File.ReadAllText(sqlFilePath);

            // Pass the string to mysqlcommand
            var mysqlCommand = new MySqlCommand(commandText, mysqlConnection);
            mysqlCommand.ExecuteNonQuery();

            await mysqlConnection.CloseAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<ClassDbContext>));

                services.AddDbContext<ClassDbContext>(options =>
                {
                    var connectionString = _mysqlContainer.GetConnectionString();
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                });
            });

            builder.UseEnvironment("Development");
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _mysqlContainer.StopAsync();
        }
    }
}
