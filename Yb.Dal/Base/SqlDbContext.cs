using Microsoft.EntityFrameworkCore;
using Yb.Model.Sys;
using Yb.Model.Cms;

namespace Yb.Dal.Base
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<YbUser> YbUsers { get; set; } = default!;
        public DbSet<News> News { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置主键（假设 Id 是字符串主键）
            modelBuilder.Entity<YbUser>().HasKey(e => e.Id);
            modelBuilder.Entity<News>().HasKey(e => e.Id);

            // 可选：设置表名
            modelBuilder.Entity<YbUser>().ToTable("YbUser");
            modelBuilder.Entity<News>().ToTable("News");

            base.OnModelCreating(modelBuilder);
        }
    }
}