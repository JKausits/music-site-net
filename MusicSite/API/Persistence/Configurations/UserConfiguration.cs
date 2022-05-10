using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).HasMaxLength(255);
            builder.Property(x => x.Password).HasMaxLength(72);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
