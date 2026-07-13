using Microsoft.EntityFrameworkCore;
using Flowerfinder.Data;
using Flowerfinder.Services;

var builder = WebApplication.CreateBuilder(args);

// Machine-local settings (real API keys live here) — this file is
// listed in .gitignore, so it never leaves this computer.
builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Photo identification (PlantNet; see the PlantNet section in appsettings.json)
builder.Services.AddHttpClient<PlantNetService>(c => c.Timeout = TimeSpan.FromSeconds(25));

// SQLite database (a single portable file: flowerapp.db)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Apply any pending migrations and fill an empty database with the starter catalog
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    SeedData.EnsureSeeded(db);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// friendly 404 page (re-executes the pipeline, keeps the 404 status)
app.UseStatusCodePagesWithReExecute("/Home/Missing");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
