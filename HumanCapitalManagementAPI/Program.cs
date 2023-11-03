namespace HumanCapitalManagementAPI
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Authentication.Cookies;

    using HumanCapitalManagementApp.Data;
    using HumanCapitalManagementApp.Services.Interfaces;
    using HumanCapitalManagementApp.Web.Infrastructure;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<HumanCapitalManagementAppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddApplicationServices(typeof(IEmployeeService));

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    // Настройки за куки аутентикацията (ако са нужни)
                });

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(policy => 
                policy.AddPolicy("WebApp", configuration =>
            {
                configuration.WithOrigins("https://localhost:7242")
                             .AllowAnyMethod()
                             .AllowAnyHeader();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var scope = app.Services.CreateScope();
            scope.ServiceProvider.GetService<HumanCapitalManagementAppDbContext>()?.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.UseCors("WebApp");

            app.Run();
        }
    }
}