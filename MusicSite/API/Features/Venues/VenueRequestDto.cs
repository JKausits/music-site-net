using System.ComponentModel.DataAnnotations;

namespace MusicSite.API.Features
{
    public class VenueRequestDto
    {
        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
