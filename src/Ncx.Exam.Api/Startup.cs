using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ncx.Exam.Api.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ncx.Exam.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Swagger;

namespace Ncx.Exam.Api
{
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // CORS should be enabled for other domain to access this API
            services.AddCors();

            // Register AppSettings as a service
            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            // Secret is used for JWT token encryption and decryption
            var secret = Encoding.ASCII.GetBytes(appSettings.Get<AppSettings>().Secret);
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var idClaim = context.Principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                            if (idClaim == null)
                            {
                                context.Fail("Bad token");
                            }
                            var userId = int.Parse(idClaim.Value);
                            var user = await userService.GetUserByIdAsync(userId);
                            if (user == null)
                            {
                                context.Fail("Bad token");
                            }
                        }
                    };
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("AppDbContext")));

            // Add Swagger service
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v1", new Info { Title = "Ncx.Exam.Api", Version = "v1" }));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Insert Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ncx.Exam.Api v1"));

            app.UseCors(policyBuilder => policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // To pass authentication, the request must contain an Authorization header formatted as:
            // Authorization: Bearer <JwtToken>
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
