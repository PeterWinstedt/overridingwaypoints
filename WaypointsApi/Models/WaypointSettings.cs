using System.ComponentModel.DataAnnotations;

namespace WaypointsApi.Models
{
    public class WaypointSettings
    {
        [Key]
        public int Id { get; set; }

        [Range(0, 300)]
        public double DefaultEnterDistance { get; set; }

        [Range(0, 300)]
        public double DefaultExitDistance { get; set; }

        [Range(0, 360)]
        public double DefaultMaxEnteringDeviationAngle { get; set; }

        public static WaypointSettings Default() => new WaypointSettings
        {
            DefaultEnterDistance = 30,
            DefaultExitDistance = 50,
            DefaultMaxEnteringDeviationAngle = 45
        };
    }
} 