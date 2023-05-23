using FlCash.Data;
using FlCash.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using FlCash.Hubs;
using System.Reflection;
using Hangfire;
using FlCash.Util;
using CorePush.Apple;
using CorePush.Google;
using FlCash.Models;
using FlCash.Services;

namespace FlCash
{
    public class Startup
    {
        UtilController utilController = new();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddControllers();
            services.AddMvc();
            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Flash Cash", Version = "v1" });
                c.DocumentFilter<SignalRSwaggerGen.SignalRSwaggerGen>(new List<Assembly> { typeof(GameHub).Assembly, typeof(GameHub).Assembly });
            });
            services.AddAutoMapper(typeof(Startup));

            // For Entity Framework  
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")), ServiceLifetime.Singleton);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("sqlConnection")));
            services.AddHangfireServer();
            // For Identity  
            services.AddIdentity<ApiUser, IdentityRole>()
                .AddEntityFrameworkStores<DatabaseContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });
            services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    );
            services.AddHttpClient<FcmSender>();
            services.AddHttpClient<ApnSender>();
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FCMNotificationSetting>(appSettingsSection);
            services.AddSingleton<DatabaseContext>();

            services.AddSingleton<IHostedService, WeeklyPointsUpdateBackgroundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [System.Obsolete]
        public void Configure(IApplicationBuilder app,IBackgroundJobClient backgroundJobClient, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Flash Cash API");
                });
                app.UseDeveloperExceptionPage();

            }
            if (env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Flash Cash API");
                });
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/HangFire/Dashboard");
            //RecurringJob.AddOrUpdate(() => utilController.ResetPointsWeekly(), Cron.Weekly(System.DayOfWeek.Friday));
            //RecurringJob.AddOrUpdate(() => utilController.ResetPointsMonthly(), Cron.Monthly(1));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/gamehub");
                endpoints.MapControllers();
                endpoints.MapAreaControllerRoute(
                 name: "Admin",
                 areaName: "Admin",
                 pattern: "Admin/{controller=Home}/{action=Index}");

            });
        }
    }
}
