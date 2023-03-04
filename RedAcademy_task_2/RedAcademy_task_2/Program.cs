#region Builder Configuration 
using Microsoft.EntityFrameworkCore;
using RedAcademy_task_2.Data;
using Microsoft.AspNetCore.Identity;
using RedAcademy_task_2.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("AspNetIdentityContextConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityContext>();

builder.Services.AddRazorPages();

#endregion

#region Configure Services 
builder.Services.AddRazorPages();
var app = builder.Build();
#endregion


#region Configure Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
#endregion

