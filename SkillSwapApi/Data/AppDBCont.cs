using Microsoft.EntityFrameworkCore;
using SkillSwapApi.Model;

namespace SkillSwapApi.Data
{
    public class AppDBCont : DbContext
    {
        public AppDBCont()
        {

        }

        public AppDBCont(DbContextOptions<AppDBCont> options) : base(options)
        {

        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Skill>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            base.OnModelCreating(modelBuilder);
        }

    }
}
