namespace HumanCapitalManagementApp
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.EntityFrameworkCore;
    
    using Data;
    using Services.Interfaces;
    using Web.Infrastructure;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //това се примести в АПИ
            //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //builder.Services.AddDbContext<HumanCapitalManagementAppDbContext>(options =>
            //    options.UseSqlServer(connectionString));


            //това не работи
            //builder.Services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = "https://localhost:7242",
            //            ValidAudience = "https://localhost:7242",
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is very Unbreakable Key believe it :)")),
            //            RoleClaimType = ClaimTypes.Role
            //            //ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //            //ValidAudience = builder.Configuration["Jwt:Audience"],
            //            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //        };
            //    });
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

            //това не работи
            //builder.Services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("UserCredentials", policy =>
            //    {
            //        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
            //        policy.RequireRole("Employee");
            //    });
            //});

            builder.Services.AddApplicationServices(typeof(IAccountService));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddControllersWithViews();


            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //това се премести в АПИ 
            //var scope = app.Services.CreateScope();
            //scope.ServiceProvider.GetService<HumanCapitalManagementAppDbContext>()?.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config =>
            {
                config.MapControllerRoute(
                    name: "areas",
                    pattern: "/{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                config.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}