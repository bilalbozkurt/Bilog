using bilog.Data;
using bilog.Services.BlogPostService;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddDbContext<DataContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("RemoteConnection"), options => options.EnableRetryOnFailure()));
builder.Services.AddScoped<IBlogPostService, BlogPostService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// app.UseCors("ApiCorsPolicy");

app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}");

app.MapFallbackToFile("index.html"); ;

app.Run();
