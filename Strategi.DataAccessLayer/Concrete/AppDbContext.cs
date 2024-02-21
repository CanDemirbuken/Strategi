using Strategi.EntityLayer;
using System.Data.Entity;

namespace Strategi.DataAccessLayer.Concrete
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=StrategiDbContext")
        {

        }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Members> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>().ToTable("Categories");
            modelBuilder.Entity<Products>().ToTable("Products");
            modelBuilder.Entity<Members>().ToTable("Members");

            base.OnModelCreating(modelBuilder);
        }
    }
}
