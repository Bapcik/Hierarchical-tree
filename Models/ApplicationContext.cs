using Microsoft.EntityFrameworkCore;

namespace testProject.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public DbSet<HierarchyModel> HierarchyNodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<HierarchyModel>()
                .HasOne<HierarchyModel>()
                .WithMany()
                .HasForeignKey(n => n.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}