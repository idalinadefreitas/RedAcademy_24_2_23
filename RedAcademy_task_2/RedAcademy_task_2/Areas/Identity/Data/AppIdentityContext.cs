using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Areas.Identity.Data;

public class AppIdentityContext : IdentityDbContext<IdentityUser>
{
    public AppIdentityContext(DbContextOptions<AppIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Assessment> Assessments { get; set; }
    public DbSet<Marketing> Marketings { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Goal> Goals { get; set; }
}
