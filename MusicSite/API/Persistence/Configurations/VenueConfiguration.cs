using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicSite.API.Persistence.Entities;

namespace MusicSite.API.Persistence.Configurations
{
    internal class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        }
    }
}
