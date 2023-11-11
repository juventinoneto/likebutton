using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Map;

namespace Infrastructure;

public class LikeButtonContext: DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Like> Likes { get; set; }

    public LikeButtonContext(DbContextOptions<LikeButtonContext> contextOptions)
        : base(contextOptions)
    {}
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
    // }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ArticleMap());
        modelBuilder.ApplyConfiguration(new LikeMap());
    }
}
