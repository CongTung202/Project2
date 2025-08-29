using Microsoft.EntityFrameworkCore;
using Project2_QuanLyTapHoa.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//connectdb
var connectionString = builder.Configuration.GetConnectionString("TapHoa");
builder.Services.AddDbContext<QuanLyTapHoaContext>(x => x.UseSqlServer(connectionString));
//session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
//
// Thêm Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // hết hạn sau 30p
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//
var app = builder.Build();
app.UseSession();

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
