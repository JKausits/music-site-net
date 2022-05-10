using System.ComponentModel.DataAnnotations;

namespace MusicSite.API.Features
{
    public class ShowRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Rate { get; set; }

        [Required]
        public DateTime StartAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime EndAt { get; set; } = DateTime.Now;
    }
}
