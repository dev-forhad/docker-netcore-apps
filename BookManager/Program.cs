using BookManager.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Database Config
var server = builder.Configuration["DatabaseServer"] ?? "";
var port = builder.Configuration["DatabasePort"] ?? "";
var user = builder.Configuration["DatabaseUser"] ?? "";
var password = builder.Configuration["DatabasePassword"] ?? "";
var database = builder.Configuration["DatabaseName"] ?? "";

var connectionString = $"Server={server},{port}; Initial Catalog={database}; Integrated Security=False; TrustServerCertificate=True; Encrypt=false; User ID={user}; Password={password}";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString));



// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// add services to DI container

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

//DatabaseManagementService.MigrationInitialization(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
