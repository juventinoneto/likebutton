using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Map;

public class ArticleMap: IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder) 
    {
        builder.ToTable("Tb_Article");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(x => x.Date)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(30);
    }
}
