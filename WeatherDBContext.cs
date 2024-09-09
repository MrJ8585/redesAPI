using Microsoft.EntityFrameworkCore;
using redesAPI.Entities;

namespace redesAPI
{
    public class WeatherDBContext : DbContext
    {
        public WeatherDBContext(DbContextOptions<WeatherDBContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherEntity> Weather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherEntity>(entity =>
            {
                entity.ToTable("weather");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();  // Indica que es serial

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .IsRequired();

                entity.Property(e => e.Temperature)
                    .HasColumnName("temperature")
                    .IsRequired();

                entity.Property(e => e.Summary)
                    .HasColumnName("summary")
                    .HasColumnType("text");
            });
        }
    }
}
