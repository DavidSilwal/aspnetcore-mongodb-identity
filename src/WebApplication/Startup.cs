using Dnx.Identity.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;
using Sakura.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.EmailTemplates;
using WebApplication.Services;
namespace WebApplication
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

     public class Startup
    {
        private readonly IHostingEnvironment _env;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                //builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            _env = env;
        }



        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Registers MongoDB conventions for ignoring default and blank fields
            // NOTE: if you have registered default conventions elsewhere, probably don't need to do this

     

            //var type = typeof(MongoIdentityUser);
            //var cm = (BsonClassMap)Activator.CreateInstance(typeof(BsonClassMap<>).MakeGenericType(type));
            //cm.AutoMap();
            //cm.SetIgnoreExtraElements(true);
            //BsonClassMap.RegisterClassMap(cm);

           // BsonClassMap.RegisterClassMap<MongoIdentityUser>(cm =>
           //{
             
           //         //cm.GetMemberMap(c => c.Id).SetElementName("_id").GetSerializer();
           //         //cm.GetMemberMap(c => c.Email).SetElementName("email").GetSerializer();
           //         //cm.GetMemberMap(c => c.UserName).SetElementName("userName").GetSerializer();
           //         //cm.GetMemberMap(c => c.NormalizedUserName).SetElementName("normalizedUserName").GetSerializer();
           //         //cm.GetMemberMap(c => c.PasswordHash).SetElementName("passwordHash").GetSerializer();
           //         //cm.GetMemberMap(c => c.SecurityStamp).SetElementName("securityStamp").GetSerializer();
           //         //cm.GetMemberMap(c => c.Logins).SetElementName("logins").GetSerializer();
           //         //cm.GetMemberMap(c => c.PhoneNumber).SetElementName("phoneNumber").GetSerializer();
           //         //cm.AutoMap();
           //         //cm.GetMemberMap(c => c.Claims).SetElementName("claims").GetSerializer();
           //    cm.SetIgnoreExtraElements(true);
           //});

            //BsonClassMap.RegisterClassMap<MongoIdentityUser>();
            //var classmap = BsonClassMap.LookupClassMap(typeof(MongoIdentityUser));
            //classmap.SetIgnoreExtraElements(true);
            //var bson = new BsonMediaTypeFormatter();
            //bson..Add(new MediaTypeFormatterCollection());


            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDb"));
           
            services.AddSingleton<IUserStore<MongoIdentityUser>>(provider =>
            {
                var options = provider.GetService<Microsoft.Extensions.Options.IOptions<MongoDbSettings>>();
                var client = new MongoClient(options.Value.ConnectionString);
                var database = client.GetDatabase(options.Value.DatabaseName);
                var loggerFactory = provider.GetService<ILoggerFactory>();

                return new MongoUserStore<MongoIdentityUser>(database, loggerFactory);
               
               });

            services.Configure<IdentityOptions>(options =>
            {
                var dataProtectionPath = Path.Combine(_env.WebRootPath, "identity-artifacts");
                options.Cookies.ApplicationCookie.AuthenticationScheme = "ApplicationCookie";
                options.Cookies.ApplicationCookie.DataProtectionProvider = Microsoft.AspNetCore.DataProtection.DataProtectionProvider.Create(dataProtectionPath);
                options.Lockout.AllowedForNewUsers = true;
            });
            // Services used by identity
            services.AddAuthentication(options =>
            {
                // This is the Default value for ExternalCookieAuthenticationScheme
                options.SignInScheme = new IdentityCookieOptions().ExternalCookieAuthenticationScheme;
            });

            services.AddBootstrapPagerGenerator(options =>
            {
                // Use default pager options.
                options.ConfigureDefault();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
                options.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions();
            services.AddDataProtection();

            services.TryAddSingleton<IdentityMarkerService>();
            services.TryAddSingleton<IUserValidator<MongoIdentityUser>, UserValidator<MongoIdentityUser>>();
            services.TryAddSingleton<IPasswordValidator<MongoIdentityUser>, PasswordValidator<MongoIdentityUser>>();
            services.TryAddSingleton<IPasswordHasher<MongoIdentityUser>, PasswordHasher<MongoIdentityUser>>();
            services.TryAddSingleton<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddSingleton<IdentityErrorDescriber>();
            services.TryAddSingleton<ISecurityStampValidator, SecurityStampValidator<MongoIdentityUser>>();
            services.TryAddSingleton<IUserClaimsPrincipalFactory<MongoIdentityUser>, UserClaimsPrincipalFactory<MongoIdentityUser>>();
            services.TryAddSingleton<UserManager<MongoIdentityUser>, UserManager<MongoIdentityUser>>();
            services.TryAddScoped<SignInManager<MongoIdentityUser>, SignInManager<MongoIdentityUser>>();

            AddDefaultTokenProviders(services);
           

            // Add framework services.
            services.AddMvc().AddJsonOptions(x =>
            {
            x.SerializerSettings.Formatting = new Newtonsoft.Json.Formatting();
            x.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            x.SerializerSettings.ContractResolver =
             new CamelCasePropertyNamesContractResolver();
        
            }); ;
            ;


            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();

            services.AddTransient<ISmsSender, AuthMessageSender>();



            services.AddScoped<IEmailTemplateEngine, EmailTemplateEngine>();
            services.AddScoped<IUserDataSource, UserDataSource>();


            services.AddSingleton<MongoUserStore<MongoIdentityUser>>(provider =>
            {
                var options = provider.GetService<IOptions<MongoDbSettings>>();
                var client = new MongoClient(options.Value.ConnectionString);
                var database = client.GetDatabase(options.Value.DatabaseName);
                var loggerFactory = provider.GetService<ILoggerFactory>();

                return new MongoUserStore<MongoIdentityUser>(database, loggerFactory);
            });

            

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app,
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory
              )
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug(LogLevel.Information);
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("~/Error/{0}");
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

          

            app.UseCookieAuthentication();
            app.UseIdentity()
               .UseFacebookAuthentication(new FacebookOptions
               {
                   AppId = "901611409868059",
                   AppSecret = "4aa3c530297b1dcebc8860334b39668b"
               })
                .UseGoogleAuthentication(new GoogleOptions
                {
                    ClientId = "514485782433-fr3ml6sq0imvhi8a7qir0nb46oumtgn9.apps.googleusercontent.com",
                    ClientSecret = "V2nDD9SkFbvLTqAUBWBBxYAL"
                })
                .UseTwitterAuthentication(new TwitterOptions
                {
                    ConsumerKey = "BSdJJ0CrDuvEhpkchnukXZBUv",
                    ConsumerSecret = "xKUNuKhsRdHD03eLn67xhPAyE1wFFEndFo1X2UJaK2m1jdAxf4"
                });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        
        private void AddDefaultTokenProviders(IServiceCollection services)
        {
            var dataProtectionProviderType = typeof(DataProtectorTokenProvider<>).MakeGenericType(typeof(MongoIdentityUser));
            var phoneNumberProviderType = typeof(PhoneNumberTokenProvider<>).MakeGenericType(typeof(MongoIdentityUser));
            var emailTokenProviderType = typeof(EmailTokenProvider<>).MakeGenericType(typeof(MongoIdentityUser));
            AddTokenProvider(services, TokenOptions.DefaultProvider, dataProtectionProviderType);
            AddTokenProvider(services, TokenOptions.DefaultEmailProvider, emailTokenProviderType);
            AddTokenProvider(services, TokenOptions.DefaultPhoneProvider, phoneNumberProviderType);
        }

        private void AddTokenProvider(IServiceCollection services, string providerName, Type provider)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.ProviderMap[providerName] = new TokenProviderDescriptor(provider);
            });

            services.AddSingleton(provider);
        }

        public class UserClaimsPrincipalFactory<TUser> : Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<TUser>
            where TUser : class
        {
            public UserClaimsPrincipalFactory(
                UserManager<TUser> userManager,
                Microsoft.Extensions.Options.IOptions<IdentityOptions> optionsAccessor)
            {
                if (userManager == null)
                {
                    throw new ArgumentNullException(nameof(userManager));
                }
                if (optionsAccessor == null || optionsAccessor.Value == null)
                {
                    throw new ArgumentNullException(nameof(optionsAccessor));
                }

                UserManager = userManager;
                Options = optionsAccessor.Value;
            }

            public UserManager<TUser> UserManager { get; private set; }

            public IdentityOptions Options { get; private set; }

            public virtual async Task<ClaimsPrincipal> CreateAsync(TUser user)
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var userId = await UserManager.GetUserIdAsync(user);
                var userName = await UserManager.GetUserNameAsync(user);
                var id = new ClaimsIdentity(Options.Cookies.ApplicationCookieAuthenticationScheme,
                    Options.ClaimsIdentity.UserNameClaimType,
                    Options.ClaimsIdentity.RoleClaimType);
                id.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
                id.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
                if (UserManager.SupportsUserSecurityStamp)
                {
                    id.AddClaim(new Claim(Options.ClaimsIdentity.SecurityStampClaimType,
                        await UserManager.GetSecurityStampAsync(user)));
                }
                if (UserManager.SupportsUserRole)
                {
                    var roles = await UserManager.GetRolesAsync(user);
                    foreach (var roleName in roles)
                    {
                        id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
                    }
                }
                if (UserManager.SupportsUserClaim)
                {
                    id.AddClaims(await UserManager.GetClaimsAsync(user));
                }

                return new ClaimsPrincipal(id);
            }
        }
    }
}
