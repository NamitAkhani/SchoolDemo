using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SchoolDemo.Models;
using SchoolDemo.Security;
using SchoolDemo.Service;
using SchoolDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDemo
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            }).AddGoogle(options =>
            {
                options.ClientId = "790280029788-i4r80bkcrf7aldva0vi27oq92qi92g09.apps.googleusercontent.com";
                options.ClientSecret = "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";
            }).AddFacebook(options =>
            {
                options.AppId = "1024549478105017";
                options.AppSecret = "4dbf9cefaa03726fee4a9ad72beb118f";
            });
           
            services.AddMvc();
            //services.AddAuthentication().AddGoogle(Options=>)
            services.AddControllers(); 
            services.AddDbContext<StudentContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StudentDBConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.SignIn.RequireConfirmedEmail = true;
                // options.Tokens.EmailConfirmationTokenProvider = "CustomEmailconfirmation";
            }).AddEntityFrameworkStores<StudentContext>()
            .AddDefaultTokenProviders();
            //.AddTokenProvider<CustomEmailConfirmationTokenProvider<IdentityUser>>("CustomEmailconfirmation");  
            
           // services.Configure<DataProtectionTokenProviderOptions>(o=>o.TokenLifespan=TimeSpan.FromHours(10));
           // services.Configure<CustomEmailConfirmationTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(1));
            services.AddControllersWithViews();
            services.AddScoped<IStudentRepository, SQLStudentRepository>();
            services.AddScoped<IUnitofWork , UnitofWork>(); 
            services.AddScoped<IGenericRepository<Student>, GenericRepository<Student>>();
            services.AddScoped<IEmailService , EmailService>();
            services.AddSingleton<IAuthorizationHandler, CanEditOnlyOtherAdminRolesAndClaimsHandler>();
            services.Configure<SMTPConfigModel>(Configuration.GetSection("SMTPConfig"));
            services.AddSingleton<DataProtectionPurposeStrings>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
                options.AddPolicy("AddRolePolicy", 
                    policy => policy.RequireClaim("Create Role"));
                options.AddPolicy("EditRolePolicy", policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));/*RequireAssertion(context=>context.User.IsInRole("Admin")&&context.User.HasClaim(claim=>claim.Type=="Edit Role" &&claim.Value=="true")|context.User.IsInRole("Super Admin")));*/
                /*RequireClaim("Edit Role").RequireRole("Admin").RequireRole("Super Admin"));*/
                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireRole("Admin"));
            });
            services.ConfigureApplicationCookie(options => 
            { 
                //options.AccessDeniedPath =new PathString("/Administration/AccessDenied");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthentication();    

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
