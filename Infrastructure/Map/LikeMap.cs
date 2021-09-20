using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Map
{
    public class LikeMap: IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder) 
        {
            builder.ToTable("Tb_Like");

            builder.HasKey(x => x.Id);
            
            builder.HasOne(x => x.Article)
                .WithMany(g => g.Likes)
                .HasForeignKey(x => x.ArticleId);

            builder.Property(x => x.ActionDate)
                .IsRequired();
        }
    }
}