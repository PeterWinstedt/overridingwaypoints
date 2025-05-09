using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WaypointsApi.Models
{
    public class Waypoint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Ref { get; set; } = string.Empty;
        public string? Extra { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [RegularExpression(@"^[A-Z]$", ErrorMessage = "Gate must be a single uppercase letter")]
        public string Gate { get; set; } = string.Empty;
        [Range(0, 360)]
        public double Direction { get; set; }
        [Range(0, 300)]
        public double EnterDistance { get; set; }
        [Range(0, 300)]
        public double ExitDistance { get; set; }
        public double Length { get; set; }
        [Required]
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public List<BorderPoint>? BorderPoints { get; set; }
    }
} 