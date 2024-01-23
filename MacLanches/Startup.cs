using MacLanches.Areas.Admin.Services;
using MacLanches.Areas.Admin.Services.Interfaces;
using MacLanches.Context;
using MacLanches.Models;
using MacLanches.Repositories;
using MacLanches.Repositories.Interfaces;
using MacLanches.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace MacLanches;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));

        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<IRelatorioVendasService, RelatorioVendasService>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
                });
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(c => CarrinhoCompra.GetCarrinho(c));

        services.AddMemoryCache();
        services.AddSession();

        services.AddControllersWithViews();

        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();

        app.UseRouting();

        //cria os perfis
        seedUserRoleInitial.SeedRoles();
        //cris os usuários e atribui ao perfil
        seedUserRoleInitial.SeedUsers();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


            endpoints.MapControllerRoute(
                name: "CategoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new { controller = "Lanche", action = "List" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}