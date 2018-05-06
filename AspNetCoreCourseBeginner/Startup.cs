using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreCourseBeginner.Data;
using AspNetCoreCourseBeginner.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreCourseBeginner
{
    public class Startup
    {
        private readonly IConfiguration _configuration;


        //My Comment: The method ConfigureServices() is NOT injectable. However, the contructor of this class is.
        //Why? Because the Program.cs class when it invoke the BuildWebHost() it also invokes the
        //CreateDefaultBuilder() which instantiates the Configuration that we need here.
        //So the Dependency Injection provider can locate that object and give us an instance of it.
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                //Need to tell the app that the scheme of authentication will be through Cookies... Set-Cookies.
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                //Now I need to force a user to authenticate before he perfroms certain actions within my website.
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
                {
                    //I am pretty much just binding my json settings variables to the options properties. Just like using AutoMapper.
                    _configuration.Bind("AzureAd", options);

                })// I will override/set up the options of this ID Connect so it knows where to connect and such. The options are in the appsettings.json
            .AddCookie();//I will take the default configuration of the cookies... Encryption, Names, etc. But I can override them if I want.




            //This is dependency injection, just like using AutoFact<>.
            //Any service that I want my application to use must be registered here.
            services.AddSingleton<IGreeter, Greeter>(); //Same instance for every request.

            //On a website with multiple users hitting your same enpoints at the same time
            //we need to make sure that those threads don't overlap with each other
            //especially when using IEnumerables<>. The best way to avoid this is by using
            //AddScoped<>(); Because it creates one instance of our DB context per HTTP request and then gets rid of it.
            services.AddScoped<IRestaurantData, SqlRestaurantData>(); //One instance per request. One instance per logical thread.


            //We are registring the DbContext into our services through Dependency Injection. However, our DB Context
            //needs a connection string which is located at our appsettings.json file (just like we do in web.config).
            //There is a configuration object that parses that JSON file into a C# class. I can use Dependency Injection
            //to get an instance of that object when this class StartUp gets created, and then access my connection string.
            //In order for the _configuration.GetConnectionString() method to get the correct connection string
            //my connection string should live under the ConnectionStrings object inside the JSON file. Then I will
            //pass the property name so it can retrieve the connection string that I want.
            services.AddDbContext<AspNetCoreCourseBeginnerDbContext>(options =>
                                 options.UseSqlServer(_configuration.GetConnectionString("AspNetCoreCourseBeginner")));

            //MVC Services
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGreeter greeter, ILogger<Startup> logger)
        {

            //Here is where we set up the MIDDLEWARE. 
            //MIDDLEWARE is pretty much the Master Configuration of our application. It controls the flow of the app
            //and what the app should do with certain requests or environments.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            //This line of code will evaluate the HTPP request and see if the user is coming from a
            //HTTP instead of HTTPS, it the user is coming from a not secure SSL (example: HTTP),
            //my app will redirect the user to a secure environment of that same link... HTTPS.
            //This way I can be sure that all the connections to my app are through SSL, even DEV, QA and PROD>
            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());

            //I want to respond with a static file for any upcoming request (static = js files, html, etc.).
            //By default this will use wwwroot folder.
            app.UseStaticFiles();

            //This extension method will make the UseStaticFiles to also look at my NPM modules.
            app.UseNodeModules(env.ContentRootPath); //ContentRootPath is the absolute location of the project.

            //Need to have this UseAuthentication() before UseMVC because I want to authenticate the user before
            //he/she goes and start playing with my MVC Controllers or Endpoints.
            app.UseAuthentication();


            //If there is no static file to handle the request I will let MVC handle it (probabbly a HTTP request to an enpoint).
            app.UseMvc(ConfigureRoutes);

           
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            //Request.. /Home/Index

            routeBuilder.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
