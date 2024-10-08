using BlogSimplesAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogSimplesAPI.Data.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasColumnType("VARCHAR(100)").IsRequired();
            builder.HasMany(x=> x.Posts)
                .WithOne(x => x.Author);
        }
    }
}
