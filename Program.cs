using System.Text;
using bilog.Data;
using bilog.Services.BlogPostService;
using bilog.Services.UserService;
using Bilog.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: "ApiCorsPolicy",
//                       builder =>
//                       {
//                           builder.AllowAnyOrigin();
//                       });
// });

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRegexUtilities, RegexUtilities>();

builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("RemoteConnection"), options => options.EnableRetryOnFailure()));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBlogPostService, BlogPostService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

// app.UseCors("ApiCorsPolicy");

app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}");

app.MapFallbackToFile("index.html"); ;

app.Run();
