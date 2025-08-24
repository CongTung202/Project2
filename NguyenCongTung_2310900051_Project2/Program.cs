using Microsoft.EntityFrameworkCore;
using NguyenCongTung_2310900051_Project2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("nctTapHoa");
builder.Services.AddDbContext<CuahangTapHoaContext>(x => x.UseSqlServer(connectionString));

var app = builder.Build();

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
    pattern: "{controller=SanPhams}/{action=Index}/{id?}");

app.Run();
