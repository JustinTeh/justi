using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext {
    //protected readonly IConfiguration Configuration;
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    // public AppDbContext(IConfiguration configuration) {
    //     Configuration = configuration;
    // }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseNpgsql(Configuration.GetConnectionString("FIWAdb"));
    // }

    /// <summary>
    /// Creates a 1-to-Many relationship between User and FridgeItem
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        // modelBuilder.Entity<User>()
        //     .HasMany(e => e.FridgeItems)
        //     .WithOne(e => e.User);
        modelBuilder.Entity<FridgeItem>()
            .HasOne(e => e.User).WithMany(e => e.FridgeItems).HasForeignKey(e => e.OwnedByUserID);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<FridgeItem> FridgeItems { get; set; }

}