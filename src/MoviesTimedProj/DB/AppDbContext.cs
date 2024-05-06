using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public AppDbContext()
    {
        // Initialize the database connection
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>()
            .HasKey(p => p.id);
    }
}