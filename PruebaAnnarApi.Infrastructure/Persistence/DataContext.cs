using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ModelWebApi.Domain.Entities;

namespace PruebaAnnarApi.Infrastructure.Persistence
{
    public class DataContext: DbContext
    {
        public DataContext()
        {
            
        }

        public DataContext(DbContextOptions<DataContext> options, IConfiguration config): base(options)
        {

        }

        public virtual DbSet<User> User { get; set; } = null!;
        public virtual DbSet<Tasks> Task { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_User_Id");
            });


            modelBuilder.Entity<Tasks>(entity =>
            {
                entity.ToTable("Task");
                entity.HasKey(e => e.Id).HasName("PK_Task_Id");
                entity.HasOne(d => d.UserNavigation)
                      .WithMany(p => p.TaskNavigation)
                      .HasForeignKey(d => d.UserId)
                      .HasConstraintName("FK_User_Task");
            });
        }

    }
}
