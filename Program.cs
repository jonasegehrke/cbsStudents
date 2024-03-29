using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using cbsStudents.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CbsStudentsContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("CbsStudentsContext")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddDefaultUI()
.AddEntityFrameworkStores<CbsStudentsContext>();


builder.Services.AddDbContext<CbsStudentsContext>(options =>
options.UseSqlite("CbsStudentsContext"));

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
