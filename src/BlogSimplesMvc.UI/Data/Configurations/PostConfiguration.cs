using BlogSimplesMvc.UI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BlogSimplesMvc.UI.Data.Configurations
{    
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("VARCHAR(450)").IsRequired();
            builder.Property(x => x.PublicationDate).IsRequired();
            builder.Property(x => x.AuthorId).IsRequired();
            
            builder.HasMany(x => x.Comments)
                .WithOne(x => x.Post)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
