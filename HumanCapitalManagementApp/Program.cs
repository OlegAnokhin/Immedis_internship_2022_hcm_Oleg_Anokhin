namespace HumanCapitalManagementApp
{
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using System.Text;
    using System.Net.Http.Headers;
    using Microsoft.AspNetCore.CookiePolicy;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMvc();
            //builder.Services.RegisterDatabase(builder.Configuration)
            //    .RegisterAuthenticationCookie();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("https://localhost:7237")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            //Old option
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
            //            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //            ValidAudience = builder.Configuration["Jwt:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            //        };
            //    });

            //builder.Services.AddHttpClient("ApiClient", client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration["http://localhost:5152"]);
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //});


            //builder.Services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //    {
            //        // Настройки за куки аутентикацията (ако са нужни)
            //    });

            //builder.Services.AddAuthorization();

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddControllersWithViews();

            //builder.Services.AddDistributedMemoryCache();

            //builder.Services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(60);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            WebApplication app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.None
            });

            app.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");

            //old options
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();

            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            //app.UseSession();

            //app.MapDefaultControllerRoute();

            app.Run();
           // app.RunAsync();
        }
    }
}