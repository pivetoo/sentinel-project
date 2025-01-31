using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sentinel.Domain.Repositories;
using Sentinel.Domain.Services;
using Sentinel.Domain.Services.Base;
using Sentinel.Domain.Services.Security;
using Sentinel.Infrastructure.Data;
using Sentinel.Infrastructure.Repositories;
using System.Text;

namespace Sentinel.IoC
{
    public static class Injection
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });

            // Repositories
            services.AddScoped(typeof(ICrudRepository<>), typeof(CrudRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            // Base Services
            services.AddScoped(typeof(CrudService<>));

            // Services
            services.AddScoped<Argon2HashingService>();
            services.AddScoped<ContratoService>();
            services.AddScoped<EmpresaService>();
            services.AddScoped<PapelService>();
            services.AddScoped<PermissaoService>();
            services.AddScoped<PlanoService>();
            services.AddScoped<UsuarioEmpresaService>();
            services.AddScoped<UsuarioSentinelService>();
        }
    }
}
