using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using RedAcademy_task_2.Models;

namespace RedAcademy_task_2.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Assessament> Assessaments { get; set; }
    }
}
