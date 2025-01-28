using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using GIHUN_MVC_Project.Core.AmazonS3;
using GIHUN_MVC_Project.Core.Couchbase;
using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Core.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserRepository, UserService>();
builder.Services.AddTransient<IReservationHotelRepository, ReservationHotelService>();

builder.Services.AddTransient<ICouchbaseRepository, CouchbaseService>();

builder.Services.AddTransient<IAmazonS3Repository, AmazonS3Service>();

builder.Services.AddCouchbase(builder.Configuration.GetSection("Couchbase"));
builder.Services.AddDistributedCouchbaseCache("Gihun-MVC-Project-Cache", opt => { });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.SlidingExpiration = true;
    opt.Cookie.HttpOnly = true;
    opt.LoginPath = "/User/Login";
});

var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                       .AddJsonFile("appsettings.json", optional: false, true)
                                       .Build();

// Configure AWS S3 Client
var awsSettings = config.GetSection("AWS");
var credential = new BasicAWSCredentials(awsSettings["AccessKeyId"], awsSettings["SecretAccessKey"]);

// Configure AWS Options
var awsOptions = config.GetAWSOptions();
awsOptions.Credentials = credential;
awsOptions.Region = RegionEndpoint.APNortheast2;
builder.Services.AddDefaultAWSOptions(awsOptions);

// Add the AWS S3 Service
builder.Services.AddAWSService<IAmazonS3>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

CookiePolicyOptions cookiePolicy = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
};

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy(cookiePolicy);
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
