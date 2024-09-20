using Microsoft.EntityFrameworkCore;
class AppDbContext : DbContext {
    protected readonly IConfiguration Configuration;
    public AppDbContext(IConfiguration configuration) {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FIWAdb"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<FridgeItem> FridgeItems {get;set;}
}