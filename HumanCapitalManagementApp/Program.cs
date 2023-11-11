namespace HumanCapitalManagementApp
{
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.CookiePolicy;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddHttpContextAccessor();
            
            builder.Services.AddMvc();
            
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

            app.Run();
        }
    }
}