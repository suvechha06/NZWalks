using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code should have minimum 3 characters")]
        [MaxLength(3, ErrorMessage = "Code Cannot exceed more than 3 characters")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
