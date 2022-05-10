using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Persistence.Configurations
{
    internal class ShowConfiguration : IEntityTypeConfiguration<Show>
    {
        public void Configure(EntityTypeBuilder<Show> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Rate).HasColumnType("decimal(12, 2)");
        }
    }
}
