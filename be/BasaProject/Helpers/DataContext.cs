namespace BasaProject.Helpers;

using Microsoft.EntityFrameworkCore;
using BasaProject.Models;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration) => Configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("BasaDB"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<MsUser> MsUsers { get; set; }
    public DbSet<MsRole> MsRoles { get; set; }
    public DbSet<MsWordList> MsWordLists { get; set; }
    public DbSet<MsBasaLemes> MsBasaLemes { get; set; }
}