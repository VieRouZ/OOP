using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SportEventApp.Models;

namespace SportEventApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Конструктор с параметрами для DI
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        // Конструктор без параметров для EF Tools
        public ApplicationDbContext()
        {
        }
        
        public DbSet<SportEvent> SportEvents { get; set; }
        public DbSet<Football> FootballEvents { get; set; }
        public DbSet<Tennis> TennisEvents { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Только если не настроено через конструктор
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SportEventDB;Username=postgres;Password=1234");
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Используем TPT (Table Per Type) - каждый класс в своей таблице
            modelBuilder.Entity<SportEvent>().ToTable("SportEvents");
            modelBuilder.Entity<Football>().ToTable("FootballEvents");
            modelBuilder.Entity<Tennis>().ToTable("TennisEvents");
            
            // Настройка свойств для Football
            modelBuilder.Entity<Football>(entity =>
            {
                entity.Property(f => f.StadiumName).HasMaxLength(100);
                entity.HasBaseType<SportEvent>();
            });
            
            // Настройка свойств для Tennis
            modelBuilder.Entity<Tennis>(entity =>
            {
                entity.Property(t => t.CourtSurface).HasMaxLength(50);
                entity.HasBaseType<SportEvent>();
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
    
    // Фабрика для EF Tools (для дизайна времени)
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SportEventDB;Username=postgres;Password=1234");
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}