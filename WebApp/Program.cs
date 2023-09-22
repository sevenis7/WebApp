using DataLayer.Context;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.PasswordHashers;
using WebAppApi.Authenticators;
using WebAppApi.TokenGenerators;
using WebAppApi.TokenValidators;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using DataLayer.Entities;
using DataLayer.Interface;

namespace WebAppApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.Configure<AuthenticationConfiguration>(builder.Configuration.GetSection("Authentication"));
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            builder.Services.AddTransient<IRequestRepository, RequestRepository>();
            builder.Services.AddTransient<IRequestService, RequestService>();
            builder.Services.AddTransient<IBaseRepository<Project>, ProjectRepository>();
            builder.Services.AddTransient<IBaseService<Project>, ProjectService>();
            builder.Services.AddTransient<IBaseRepository<Service>,  ServiceRepository>();
            builder.Services.AddTransient<IBaseService<Service>,  ServiceSerivce>();
            builder.Services.AddTransient<IBaseRepository<Article>, ArticleRepository>();
            builder.Services.AddTransient<IBaseService<Article>, ArticleService>();
            builder.Services.AddTransient<IBaseRepository<ContactLink>, ContactLinkRepository>();
            builder.Services.AddTransient<IBaseService<ContactLink>, ContactLinkService>();
            builder.Services.AddTransient<IContent, Content>();
            builder.Services.AddTransient<IContentService, ContentService>();
            builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
            builder.Services.AddSingleton<AccessTokenGenerator>();
            builder.Services.AddSingleton<RefreshTokenGenerator>();
            builder.Services.AddSingleton<TokenGenerator>();
            builder.Services.AddSingleton<RefreshTokenValidator>();
            builder.Services.AddTransient<Authenticator>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {

                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:AccessTokenSecret"])),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            var app = builder.Build();

            app.UseCors(builder => builder.AllowAnyOrigin()
                             .AllowAnyHeader()
                            .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}